using FluentResults;
using System.Globalization;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;

namespace TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;

public interface ICypherBDecisionService
{
    IResult<Decision> MakeDecision(CypherBDecisionRequest request);
}

public record CypherBDecisionRequest(IEnumerable<CypherBQuote> Quotes, Granularity Granularity, WaveTrendSettings WaveTrendSettings);

public class CypherBDecisionService : ICypherBDecisionService
{
    public IResult<Decision> MakeDecision(CypherBDecisionRequest request)
    {
        var maxSignalAgeResult = Minutes.GetMaxSignalAge(request.Granularity);
        if (maxSignalAgeResult.IsFailed)
        {
            return maxSignalAgeResult.ToResult<Decision>();
        }


        var latestQuote = request.Quotes.LastOrDefault();
        if (latestQuote == null)
        {
            return Result.Fail<Decision>(new ValidationError("Quotes is empty"));
        }

        var decision = Decision.CreateNew(
            new IndexOutcome("CipherB", null, GetAdditionalParams(latestQuote)),
            DateTime.UtcNow,
            GetCumulativeTradeAction(request.Quotes, latestQuote, maxSignalAgeResult.Value, request.WaveTrendSettings),
            GetMarketDirection()
        );
        return Result.Ok(decision);
    }

    private static MarketDirection GetMarketDirection()
    {
        return MarketDirection.Bullish;
    }

    private static Dictionary<string, string> GetAdditionalParams(CypherBQuote latestQuote) => new()
    {
        {
            nameof(latestQuote.WaveTrend.Wt1),
            latestQuote.WaveTrend.Wt1.ToString(CultureInfo.InvariantCulture)
        },
        {
            nameof(latestQuote.WaveTrend.Wt2),
            latestQuote.WaveTrend.Wt2.ToString(CultureInfo.InvariantCulture)
        },
        { nameof(latestQuote.WaveTrend.Vwap), latestQuote.WaveTrend.Vwap.ToString() },
        {
            nameof(latestQuote.Mfi.Mfi),
            latestQuote.Mfi.Mfi.ToString(CultureInfo.InvariantCulture)
        }
    };

    private static TradeAction GetCumulativeTradeAction(IEnumerable<CypherBQuote> quotes, CypherBQuote latestQuote, Minutes maxSignalAge, WaveTrendSettings WaveTrendSettings)
    {
        var vwapTradeAction = GeVwapTradeAction(latestQuote);
        var wtTradeAction = GetWtSignalsTradeAction(quotes, latestQuote, maxSignalAge);
        var mfiTradeAction = GeMfiTradeAction(latestQuote, WaveTrendSettings);
        TradeAction[] tradeActions = [vwapTradeAction, wtTradeAction, mfiTradeAction];

        if (Array.TrueForAll(tradeActions, x => x == TradeAction.Buy))
            return TradeAction.Buy;
        return Array.TrueForAll(tradeActions, x => x == TradeAction.Sell) ? TradeAction.Sell : TradeAction.Hold;
    }

    private static TradeAction GetWtSignalsTradeAction(IEnumerable<CypherBQuote> quotes, CypherBQuote latestQuote, Minutes maxSignalAge)
    {

        foreach (var quote in quotes.OrderByDescending(q => q.Ohlc.Date))
        {
            var signalAgeInMinutes = (latestQuote.Ohlc.Date - quote.Ohlc.Date).TotalMinutes;
            if (signalAgeInMinutes > maxSignalAge.Value)
                break;

            if (quote.WaveTrend.CrossesOver == true)
            {
                return TradeAction.Buy;
            }
            if (quote.WaveTrend.CrossesUnder == true)
            {
                return TradeAction.Sell;
            }
        }

        return TradeAction.Hold;
    }

    private static TradeAction GeVwapTradeAction(CypherBQuote quote) =>
        quote.WaveTrend.Vwap > 0 ? TradeAction.Buy : TradeAction.Sell;

    // Mfi has different scale
    private static TradeAction GeMfiTradeAction(CypherBQuote quote, WaveTrendSettings waveTrendSettings)
    {
        if (quote.Mfi.Mfi > waveTrendSettings.Overbought)
        {
            return TradeAction.Sell;
        }

        if (quote.Mfi.Mfi < waveTrendSettings.Oversold)
        {
            return TradeAction.Buy;
        }

        return TradeAction.Hold;
    }

}

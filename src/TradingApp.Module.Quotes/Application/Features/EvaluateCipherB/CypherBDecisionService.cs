using FluentResults;
using System.Globalization;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;

namespace TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;

public interface ICypherBDecisionService
{
    IResult<Decision> MakeDecision(IEnumerable<CypherBQuote> quotes, Granularity granularity);
}

public class CypherBDecisionService : ICypherBDecisionService
{
    public IResult<Decision> MakeDecision(IEnumerable<CypherBQuote> quotes, Granularity granularity)
    {
        var maxSignalAgeResult = GetMaxSignalAge(granularity);
        if (maxSignalAgeResult.IsFailed)
        {
            return maxSignalAgeResult.ToResult<Decision>();
        }


        var latestQuote = quotes.Last();

        var decision = Decision.CreateNew(
            new IndexOutcome("CipherB", 0, GetAdditionalParams(latestQuote)),
            DateTime.UtcNow,
            GetCumulativeTradeAction(quotes, latestQuote, maxSignalAgeResult.Value),
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

    private static TradeAction GetCumulativeTradeAction(IEnumerable<CypherBQuote> quotes, CypherBQuote latestQuote, Minutes maxSignalAge)
    {
        var vwapTradeAction = GeVwapTradeAction(latestQuote);
        var wtTradeAction = GetWtSignalsTradeAction(quotes, latestQuote, maxSignalAge);
        TradeAction[] tradeActions = [vwapTradeAction, wtTradeAction];

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

    public static Result<Minutes> GetMaxSignalAge(Granularity granularity) =>
        granularity switch
        {
            Granularity.Daily => Result.Ok(Minutes.FromMinutes(5)),
            Granularity.Hourly => Result.Ok(Minutes.FromHours(5)),
            Granularity.FiveMins => Result.Ok(Minutes.FromDays(5)),
            _ => Result.Fail<Minutes>(new ValidationError($"Granularity out of scope: {granularity}"))
        };

    public readonly record struct Minutes
    {
        public int Value { get; }

        private Minutes(int value)
        {
            Value = value;
        }

        public static Minutes FromMinutes(int minutes) => new(minutes);

        public static Minutes FromHours(int hours) => new(hours * 60);
        public static Minutes FromDays(int days) => new(days * 60 * 24);
    }
}

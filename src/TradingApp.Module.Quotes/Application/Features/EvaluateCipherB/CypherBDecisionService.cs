using FluentResults;
using System.Globalization;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Application.Features.TradeSignals;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.CipherB;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;

namespace TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;

public record struct CypherBDecisionSettings(Granularity Granularity, WaveTrendSettings WaveTrendSettings, MfiSettings MfiSettings, SrsiSettings? SrsiSettings, TradingStrategy TradingStrategy);

public interface ICypherBDecisionService
{
    Result<Decision> MakeDecision(IReadOnlyList<Quote> quotes, CypherBDecisionSettings settings);
}

public class CypherBDecisionService : ICypherBDecisionService
{
    private readonly ICipherBStrategy _cipherBStrategy;
    public CypherBDecisionService(ICipherBStrategy cipherBStrategy)
    {
        ArgumentNullException.ThrowIfNull(cipherBStrategy);
        _cipherBStrategy = cipherBStrategy;
    }

    public Result<Decision> MakeDecision(IReadOnlyList<Quote> quotes, CypherBDecisionSettings settings)
    {
        var result = _cipherBStrategy.EvaluateSignals(quotes, settings);
        if (result.IsFailed)
        {
            return result.ToResult();
        }
        var (mfiResults, waveTrendSignals, _) = result.Value;
        var latestWtQuote = waveTrendSignals.ElementAtOrDefault(^1);
        var latestMfiQuote = mfiResults.ElementAtOrDefault(^1);
        if (latestWtQuote == null || latestMfiQuote == null)
        {
            return Result.Fail<Decision>(new ValidationError("Quotes is empty"));
        }

        var decision = Decision.CreateNew(
            new IndexOutcome("CipherB", null, GetAdditionalParams(latestMfiQuote, latestWtQuote)),
            DateTime.UtcNow,
            GetCumulativeTradeAction(latestMfiQuote, latestWtQuote, settings.WaveTrendSettings),
            MarketDirection.Bullish
        );
        return Result.Ok(decision);
    }

    private static Dictionary<string, string> GetAdditionalParams(MfiResult mfiResult, WaveTrendSignal waveTrendResult) => new()
    {
        {
            nameof(waveTrendResult.Wt1),
            waveTrendResult.Wt1.ToString(CultureInfo.InvariantCulture)
        },
        {
            nameof(waveTrendResult.Wt2),
            waveTrendResult.Wt2.ToString(CultureInfo.InvariantCulture)
        },
        { nameof(waveTrendResult.Vwap), waveTrendResult.Vwap.ToString() },
        {
            nameof(mfiResult.Mfi),
            mfiResult.Mfi.ToString(CultureInfo.InvariantCulture)
        }
    };

    private static TradeAction GetCumulativeTradeAction(MfiResult mfiResult, WaveTrendSignal waveTrendResult, WaveTrendSettings waveTrendSettings)
    {
        var vwapTradeAction = VwapSignals.GeVwapTradeAction(waveTrendResult);
        var mfiTradeAction = MfiSignals.GeMfiTradeAction(mfiResult, waveTrendSettings);
        TradeAction[] tradeActions = [vwapTradeAction, waveTrendResult.TradeAction, mfiTradeAction];

        if (Array.TrueForAll(tradeActions, x => x == TradeAction.Buy))
            return TradeAction.Buy;
        return Array.TrueForAll(tradeActions, x => x == TradeAction.Sell) ? TradeAction.Sell : TradeAction.Hold;
    }
}

using FluentResults;
using System.Globalization;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Application.Features.TradeSignals;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;
using WaveTrendSignals = TradingApp.Module.Quotes.Application.Features.TradeSignals.WaveTrendSignals;

namespace TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;

public record struct CypherBDecisionSettings(Granularity Granularity, WaveTrendSettings WaveTrendSettings, MfiSettings MfiSettings, SRsiSettings SrsiSettings);

public interface ICypherBDecisionService
{
    Result<Decision> MakeDecision(IReadOnlyList<Quote> quotes, CypherBDecisionSettings settings);
    Result<IReadOnlyList<CypherBQuote>> GetDecisionQuotes(
        IReadOnlyList<Quote> quotes,
        CypherBDecisionSettings decisionSettings
    );
}

public class CypherBDecisionService : ICypherBDecisionService
{
    private readonly IEvaluator _evaluator;
    private readonly ISrsiStrategyFactory _srsiStrategyFactory;
    public CypherBDecisionService(IEvaluator evaluator, ISrsiStrategyFactory srsiStrategyFactory)
    {
        ArgumentNullException.ThrowIfNull(evaluator);
        ArgumentNullException.ThrowIfNull(srsiStrategyFactory);
        _evaluator = evaluator;
        _srsiStrategyFactory = srsiStrategyFactory;
    }
    public Result<IReadOnlyList<CypherBQuote>> GetDecisionQuotes(IReadOnlyList<Quote> quotes, CypherBDecisionSettings settings)
    {
        var result = EvaluateCipherB(quotes, settings);
        if (result.IsFailed)
        {
            return result.ToResult();
        }
        var (mfiResults, waveTrendSignals, srsiSignals, _) = result.Value;
        return quotes
            .Select((q, i) => new CypherBQuote(
                q,
                waveTrendSignals.ElementAtOrDefault(i),
                mfiResults.ElementAtOrDefault(i), srsiSignals.ElementAtOrDefault(i))
            )
            .ToList();
    }
    public Result<Decision> MakeDecision(IReadOnlyList<Quote> quotes, CypherBDecisionSettings settings)
    {
        var result = EvaluateCipherB(quotes, settings);
        if (result.IsFailed)
        {
            return result.ToResult();
        }
        var (mfiResults, waveTrendSignals, _, maxSignalAgeResult) = result.Value;
        var latestWtQuote = waveTrendSignals[^1];
        var latestMfiQuote = mfiResults[^1];
        if (latestWtQuote == null || latestMfiQuote == null)
        {
            return Result.Fail<Decision>(new ValidationError("Quotes is empty"));
        }

        var decision = Decision.CreateNew(
            new IndexOutcome("CipherB", null, GetAdditionalParams(latestMfiQuote, latestWtQuote)),
            DateTime.UtcNow,
            GetCumulativeTradeAction(quotes, latestMfiQuote, latestWtQuote, maxSignalAgeResult, settings.WaveTrendSettings),
            GetMarketDirection()
        );
        return Result.Ok(decision);
    }

    private Result<(
        IReadOnlyList<MfiResult> mfiResults,
        IReadOnlyList<WaveTrendSignal> waveTrendSignals,
        IReadOnlyList<SrsiSignal> srsiSignals,
        Minutes maxSignalAgeResult
        )> EvaluateCipherB(IReadOnlyList<Quote> quotes, CypherBDecisionSettings settings)
    {
        var maxSignalAgeResult = Minutes.GetMaxSignalAge(settings.Granularity);
        if (maxSignalAgeResult.IsFailed)
        {
            return maxSignalAgeResult.ToResult();
        }

        var waveTrendResults = _evaluator.GetWaveTrend(quotes, settings.WaveTrendSettings);
        if (waveTrendResults.Count < 2)
        {
            return Result.Fail("Quotes can not be less than 2 elements");
        }
        var waveTrendSignals = WaveTrendSignals.CreateWaveTrendSignals(waveTrendResults, settings.WaveTrendSettings);
        var mfiResults = _evaluator.GetMfi(quotes, settings.MfiSettings);
        if (mfiResults.Count < 2)
        {
            return Result.Fail("Quotes can not be less than 2 elements");
        }
        var strategy = _srsiStrategyFactory.GetStrategy(TradingStrategy.EmaAndStoch);
        var srsiSignals = strategy.EvaluateSignals(quotes);
        if (srsiSignals.IsFailed)
        {
            return srsiSignals.ToResult();
        }
        return Result.Ok((mfiResults, waveTrendSignals, srsiSignals.Value, maxSignalAgeResult.Value));
    }

    private static MarketDirection GetMarketDirection()
    {
        return MarketDirection.Bullish;
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

    private static TradeAction GetCumulativeTradeAction(IReadOnlyList<Quote> quotes, MfiResult mfiResult, WaveTrendSignal waveTrendResult, Minutes maxSignalAge, WaveTrendSettings waveTrendSettings)
    {
        var vwapTradeAction = VwapSignals.GeVwapTradeAction(waveTrendResult);
        var wtTradeAction = WaveTrendSignals.GetWtSignalsTradeAction(quotes, waveTrendResult, maxSignalAge);
        var mfiTradeAction = MfiSignals.GeMfiTradeAction(mfiResult, waveTrendSettings);
        TradeAction[] tradeActions = [vwapTradeAction, wtTradeAction, mfiTradeAction];

        if (Array.TrueForAll(tradeActions, x => x == TradeAction.Buy))
            return TradeAction.Buy;
        return Array.TrueForAll(tradeActions, x => x == TradeAction.Sell) ? TradeAction.Sell : TradeAction.Hold;
    }
}

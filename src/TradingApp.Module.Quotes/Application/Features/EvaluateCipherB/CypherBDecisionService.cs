using FluentResults;
using System.Globalization;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;
using TradingApp.Module.Quotes.Application.Features.TradeSignals;
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
    IResult<Decision> MakeDecision(IReadOnlyList<Quote> quotes, CypherBDecisionSettings settings);
    Result<IReadOnlyList<CypherBQuote>> GetQuotesTradeActions(
        IReadOnlyList<Quote> quotes,
        CypherBDecisionSettings decisionSettings
    );
}

public class CypherBDecisionService : ICypherBDecisionService
{
    private readonly IEvaluator _evaluator;
    private readonly ISrsiDecisionService _srsiDecisionService;
    public CypherBDecisionService(IEvaluator evaluator, ISrsiDecisionService srsiDecisionService)
    {
        ArgumentNullException.ThrowIfNull(evaluator);
        ArgumentNullException.ThrowIfNull(srsiDecisionService);
        _evaluator = evaluator;
        _srsiDecisionService = srsiDecisionService;
    }
    public Result<IReadOnlyList<CypherBQuote>> GetQuotesTradeActions(IReadOnlyList<Quote> quotes, CypherBDecisionSettings decisionSettings)
    {
        var waveTrendResults = _evaluator.GetWaveTrend(quotes, decisionSettings.WaveTrendSettings);
        var waveTrendSignals = WaveTrendSignals.CreateWaveTrendSignals(waveTrendResults, decisionSettings.WaveTrendSettings);
        var mfi = _evaluator.GetMfi(
            quotes,
            decisionSettings.MfiSettings
        );
        var srsiSignals = _srsiDecisionService.GetQuotesTradeActions(
            quotes,
            new SrsiDecisionSettings(decisionSettings.SrsiSettings, 50m, 100m)// change emas
        );
        if (srsiSignals.IsFailed)
        {
            return srsiSignals.ToResult();
        }
        return quotes
            .Select((q, i) => new CypherBQuote(
                q,
                waveTrendSignals.ElementAtOrDefault(i),
                mfi.ElementAtOrDefault(i), srsiSignals.Value.ElementAtOrDefault(i))
            )
            .ToList();
    }
    public IResult<Decision> MakeDecision(IReadOnlyList<Quote> quotes, CypherBDecisionSettings settings)
    {
        var maxSignalAgeResult = Minutes.GetMaxSignalAge(settings.Granularity);
        if (maxSignalAgeResult.IsFailed)
        {
            return maxSignalAgeResult.ToResult<Decision>();
        }

        var waveTrendResults = _evaluator.GetWaveTrend(quotes, settings.WaveTrendSettings);
        var wtQuotes = WaveTrendSignals.CreateWaveTrendSignals(waveTrendResults, settings.WaveTrendSettings);
        var mfiQuotes = _evaluator.GetMfi(quotes, settings.MfiSettings);
        var latestWtQuote = wtQuotes.LastOrDefault();
        var latestMfiQuote = mfiQuotes.LastOrDefault();
        if (latestWtQuote == null || latestMfiQuote == null)
        {
            return Result.Fail<Decision>(new ValidationError("Quotes is empty"));
        }

        var decision = Decision.CreateNew(
            new IndexOutcome("CipherB", null, GetAdditionalParams(latestMfiQuote, latestWtQuote)),
            DateTime.UtcNow,
            GetCumulativeTradeAction(quotes, latestMfiQuote, latestWtQuote, maxSignalAgeResult.Value, settings.WaveTrendSettings),
            GetMarketDirection()
        );
        return Result.Ok(decision);
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

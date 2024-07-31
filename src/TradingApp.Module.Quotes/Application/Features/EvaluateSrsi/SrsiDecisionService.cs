using FluentResults;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Constants;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;

namespace TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;

public record struct SrsiDecisionSettings(decimal Ema, decimal Ema2X);

public interface ISrsiDecisionService
{
    /// <summary>
    /// Get Decision for latest date
    /// </summary>
    /// <param name="quotes"></param>
    /// <param name="srsiDecisionSettings"></param>
    /// <param name="sRsiSettings"></param>
    /// <returns></returns>
    Result<Decision> MakeDecision(
        IReadOnlyList<Quote> quotes,
        SrsiDecisionSettings srsiDecisionSettings,
        SRsiSettings sRsiSettings
    );

    /// <summary>
    /// Get list of decisions in the past
    /// </summary>
    /// <param name="quotes"></param>
    /// <param name="srsiDecisionSettings"></param>
    /// <param name="sRsiSettings"></param>
    /// <returns></returns>
    Result<IEnumerable<Decision>> GetQuotesTradeActions(
        IReadOnlyList<Quote> quotes,
        SrsiDecisionSettings srsiDecisionSettings,
        SRsiSettings sRsiSettings
    );
}

public class SrsiDecisionService : ISrsiDecisionService
{
    private readonly IEvaluator _evaluator;

    public SrsiDecisionService(IEvaluator evaluator)
    {
        ArgumentNullException.ThrowIfNull(evaluator);
        _evaluator = evaluator;
    }

    public Result<Decision> MakeDecision(
        IReadOnlyList<Quote> quotes,
        SrsiDecisionSettings srsiDecisionSettings,
        SRsiSettings sRsiSettings
    )
    {
        var srsiResults = _evaluator.GetSrsi(quotes, sRsiSettings);
        if (srsiResults.Count < 2)
        {
            return Result.Fail("Quotes can not be less than 2 elements");
        }
        var last = srsiResults[^1];
        var penult = srsiResults[^2];
        var additionalParams = new Dictionary<string, string>
        {
            { nameof(last.StochK), last.StochK.ToString() },
            { nameof(last.StochD), last.StochD.ToString() }
        };
        var decision = Decision.CreateNew(
            new IndexOutcome(IndexNames.Srsi, null, additionalParams),
            DateTime.UtcNow,
            GetTradeAction(quotes[^1].Close, last, penult, sRsiSettings, srsiDecisionSettings),
            MarketDirection.Bullish
        );
        return decision;
    }

    public Result<IEnumerable<Decision>> GetQuotesTradeActions(
        IReadOnlyList<Quote> quotes,
        SrsiDecisionSettings srsiDecisionSettings,
        SRsiSettings sRsiSettings
    )
    {
        return Result.Fail("Not Implemented");
    }

    private static TradeAction GetTradeAction(
        decimal latestClose,
        SRsiResult last,
        SRsiResult penult,
        SRsiSettings sRsiSettings,
        SrsiDecisionSettings srsiDecisionSettings
    ) =>
        (latestClose, last, penult, sRsiSettings, srsiDecisionSettings) switch
        {
            var (c, l, p, s, sd) when SellSignal(c, l, p, s, sd) => TradeAction.Sell,
            var (c, l, p, s, sd) when BuySignal(c, l, p, s, sd) => TradeAction.Buy,
            _ => TradeAction.Hold
        };

    private static bool SellSignal(
        decimal latestClose,
        SRsiResult last,
        SRsiResult penult,
        SRsiSettings sRsiSettings,
        SrsiDecisionSettings srsiDecisionSettings
    ) =>
        KSDellSignal(last, penult, sRsiSettings)
        && !KDBuySignal(last, penult, sRsiSettings)
        && EmaSell(srsiDecisionSettings)
        && ClosePriceSell(latestClose, srsiDecisionSettings);

    private static bool BuySignal(
        decimal latestClose,
        SRsiResult last,
        SRsiResult penult,
        SRsiSettings sRsiSettings,
        SrsiDecisionSettings srsiDecisionSettings
    ) =>
        !KSDellSignal(last, penult, sRsiSettings)
        && KDBuySignal(last, penult, sRsiSettings)
        && EmaBuy(srsiDecisionSettings)
        && ClosePriceBuy(latestClose, srsiDecisionSettings);

    private static bool KSDellSignal(
        SRsiResult last,
        SRsiResult penult,
        SRsiSettings sRsiSettings
    ) =>
        penult.StochK > sRsiSettings.Overbought
        && last.StochK < sRsiSettings.Overbought
        && penult.StochK > penult.StochD
        && last.StochK < last.StochD;

    private static bool KDBuySignal(
        SRsiResult last,
        SRsiResult penult,
        SRsiSettings sRsiSettings
    ) =>
        penult.StochK < sRsiSettings.Oversold
        && last.StochK > sRsiSettings.Oversold
        && penult.StochK < penult.StochD
        && last.StochK > last.StochD;

    private static bool EmaSlopingDownward(SrsiDecisionSettings srsiDecisionSettings) =>
        srsiDecisionSettings.Ema > srsiDecisionSettings.Ema
        && srsiDecisionSettings.Ema2X > srsiDecisionSettings.Ema2X;

    private static bool EmaSlopingUpward(SrsiDecisionSettings srsiDecisionSettings) =>
        srsiDecisionSettings.Ema < srsiDecisionSettings.Ema
        && srsiDecisionSettings.Ema2X < srsiDecisionSettings.Ema2X;

    private static bool EmaSell(SrsiDecisionSettings srsiDecisionSettings) =>
        srsiDecisionSettings.Ema < srsiDecisionSettings.Ema2X
        && EmaSlopingDownward(srsiDecisionSettings);

    private static bool ClosePriceSell(
        decimal latestClose,
        SrsiDecisionSettings srsiDecisionSettings
    ) => latestClose < srsiDecisionSettings.Ema;

    private static bool ClosePriceBuy(
        decimal latestClose,
        SrsiDecisionSettings srsiDecisionSettings
    ) => latestClose > srsiDecisionSettings.Ema;

    private static bool EmaBuy(SrsiDecisionSettings srsiDecisionSettings) =>
        srsiDecisionSettings.Ema > srsiDecisionSettings.Ema2X
        && EmaSlopingUpward(srsiDecisionSettings);
}

//https://www.liteforex.pl/blog/for-beginners/najlepsze-wskazniki-forex/oscylator-stochastyczny/

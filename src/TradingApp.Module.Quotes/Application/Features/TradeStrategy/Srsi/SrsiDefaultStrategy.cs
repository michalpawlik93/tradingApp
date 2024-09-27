using FluentResults;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Enums;

namespace TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;

// todo: add SideIndices as part of request
public class SrsiDefaultStrategy : ISrsiStrategy
{
    private readonly IEvaluator _evaluator;
    private const int DecimalPlace = 4;

    private static SrsiSettings FastSettings => new(true, 3, 10, 7, 30, 70);

    public SrsiDefaultStrategy(IEvaluator evaluator)
    {
        ArgumentNullException.ThrowIfNull(evaluator);
        _evaluator = evaluator;
    }

    /// <summary>
    /// Scalping is high frequency strategy for 5min time frame
    /// Slow not needed
    /// Long
    ///  - %K crosses %D down to up
    ///  - %K and %D are below oversold level
    ///  Short
    ///  - %K crosses %D up to down
    ///  - %K and %D are above overbought level
    /// </summary>
    /// <returns></returns>
    public Result<IReadOnlyList<SrsiSignal>> EvaluateSignals(IReadOnlyList<Quote> quotes, SrsiSettings? customSettings = null)
    {
        if (customSettings is { Enabled: false })
        {
            return Result.Ok((IReadOnlyList<SrsiSignal>)[]);
        }
        var srsiResults = _evaluator.GetSrsi(quotes, customSettings ?? FastSettings);
        if (srsiResults.Count < 2)
        {
            return Result.Fail("Quotes can not be less than 2 elements");
        }
        var signals = CreateSriSignals(srsiResults, customSettings ?? FastSettings);
        return Result.Ok(signals);
    }

    private static IReadOnlyList<SrsiSignal> CreateSriSignals(
        IReadOnlyList<SRsiResult> srsiResults,
        SrsiSettings sRsiSettings
    )
    {
        var results = new List<SrsiSignal>(srsiResults.Count);
        for (var i = 0; i < srsiResults.Count; i++)
        {
            if (i == 0)
            {
                results.Add(new SrsiSignal(0, 0, TradeAction.Hold));
                continue;
            }
            results.Add(
                srsiResults[i].StochD.HasValue
                && srsiResults[i].StochK.HasValue
                && srsiResults[i - 1].StochD.HasValue
                && srsiResults[i - 1].StochK.HasValue
                    ? new SrsiSignal(
                        Math.Round(srsiResults[i].StochK.Value, DecimalPlace),
                        Math.Round(srsiResults[i].StochD.Value, DecimalPlace),
                        GetTradeAction(srsiResults[i], srsiResults[i - 1], sRsiSettings)
                    )
                    : new SrsiSignal(0, 0, TradeAction.Hold)
            );
        }

        return results;
    }

    public static TradeAction GetTradeAction(
        SRsiResult last,
        SRsiResult penult,
        SrsiSettings sRsiSettings
    )
    {
        if (SellSignal(last, penult, sRsiSettings))
        {
            return TradeAction.Sell;
        }
        return BuySignal(last, penult, sRsiSettings) ? TradeAction.Buy : TradeAction.Hold;
    }

    private static bool SellSignal(SRsiResult last, SRsiResult penult, SrsiSettings sRsiSettings) =>
        SrsiStrategyExtensions.KDSellSignal(last, penult, sRsiSettings)
        && !SrsiStrategyExtensions.KDBuySignal(last, penult, sRsiSettings);

    private static bool BuySignal(SRsiResult last, SRsiResult penult, SrsiSettings sRsiSettings) =>
        SrsiStrategyExtensions.KDBuySignal(last, penult, sRsiSettings)
        && !SrsiStrategyExtensions.KDSellSignal(last, penult, sRsiSettings);
}

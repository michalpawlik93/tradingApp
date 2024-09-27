using FluentResults;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Enums;

namespace TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;

// todo: add SideIndices as part of request
public class Srsi1hEurPlnStrategy : ISrsiStrategy
{
    private readonly IEvaluator _evaluator;

    private static SrsiSettings FastSettings => new(true, 3, 5, 3, 20, 80);
    private static SrsiSettings SlowSettings => new(true, 7, 21, 7, 20, 80);
    private const int DecimalPlace = 4;

    public Srsi1hEurPlnStrategy(IEvaluator evaluator)
    {
        ArgumentNullException.ThrowIfNull(evaluator);
        _evaluator = evaluator;
    }

    /// <summary>
    /// DayTrading is when open and close position is at the same day. For 1hour, 30m or less time frame
    /// Fast stoch (3.5.3), Slow stoch (7,21,7)
    /// Long
    ///  - Slow stoch is upward (bullish), %K sloping upward
    ///  - %K crosses %D for fast Stoch down to up
    ///  - %K and %D for fast Stoch are below oversold level
    ///  Short
    ///  - Slow stoch is downward (bearish)  %K sloping downward
    ///  - %K crosses %D for fast Stoch up to down
    ///  - %K and %D for fast Stoch are above overbought level
    /// </summary>
    /// <returns></returns>
    public Result<IReadOnlyList<SrsiSignal>> EvaluateSignals(
        IReadOnlyList<Quote> quotes,
        SrsiSettings? customSettings = null
    )
    {
        if (customSettings != null)
        {
            return Result.Fail(
                new ValidationError(
                    $"Can not call {nameof(Srsi1hEurPlnStrategy)} with custom settings."
                )
            );
        }

        var srsiFastResults = _evaluator.GetSrsi(quotes, FastSettings);
        if (srsiFastResults.Count < 2)
        {
            return Result.Fail("Quotes can not be less than 2 elements");
        }

        var srsiSlowResults = _evaluator.GetSrsi(quotes, SlowSettings);
        if (srsiSlowResults.Count < 2)
        {
            return Result.Fail("Quotes can not be less than 2 elements");
        }
        var signals = CreateSriSignals(srsiFastResults, srsiSlowResults, FastSettings);
        return Result.Ok(signals);
    }

    private static IReadOnlyList<SrsiSignal> CreateSriSignals(
        IReadOnlyList<SRsiResult> srsiFastResults,
        IReadOnlyList<SRsiResult> srsiSlowResults,
        SrsiSettings srsiSettings
    )
    {
        var results = new List<SrsiSignal>(srsiFastResults.Count);
        for (var i = 0; i < srsiFastResults.Count; i++)
        {
            if (i == 0)
            {
                results.Add(new SrsiSignal(0, 0, TradeAction.Hold));
                continue;
            }
            results.Add(
                srsiFastResults[i].StochD.HasValue
                && srsiFastResults[i].StochK.HasValue
                && srsiFastResults[i - 1].StochD.HasValue
                && srsiFastResults[i - 1].StochK.HasValue
                    ? new SrsiSignal(
                        Math.Round(srsiFastResults[i].StochK.Value, DecimalPlace),
                        Math.Round(srsiFastResults[i].StochD.Value, DecimalPlace),
                        GetTradeAction(
                            srsiFastResults[i],
                            srsiFastResults[i - 1],
                            srsiSlowResults[i],
                            srsiSlowResults[i - 1],
                            srsiSettings
                        )
                    )
                    : new SrsiSignal(0, 0, TradeAction.Hold)
            );
        }

        return results;
    }

    public static TradeAction GetTradeAction(
        SRsiResult lastFast,
        SRsiResult penultFast,
        SRsiResult lastSlow,
        SRsiResult penultSlow,
        SrsiSettings srsiSettings
    )
    {
        if (SellSignal(lastFast, penultFast, lastSlow, penultSlow, srsiSettings))
        {
            return TradeAction.Sell;
        }
        return BuySignal(lastFast, penultFast, lastSlow, penultSlow, srsiSettings)
            ? TradeAction.Buy
            : TradeAction.Hold;
    }

    private static bool SellSignal(
        SRsiResult lastFast,
        SRsiResult penultFast,
        SRsiResult lastSlow,
        SRsiResult penultSlow,
        SrsiSettings srsiSettings
    ) =>
        SrsiStrategyExtensions.KDSellSignal(lastFast, penultFast, srsiSettings)
        && !SrsiStrategyExtensions.KDBuySignal(lastFast, penultFast, srsiSettings)
        && BearishTrend(lastSlow, penultSlow);

    private static bool BuySignal(
        SRsiResult lastFast,
        SRsiResult penultFast,
        SRsiResult lastSlow,
        SRsiResult penultSlow,
        SrsiSettings srsiSettings
    ) =>
        SrsiStrategyExtensions.KDBuySignal(lastFast, penultFast, srsiSettings)
        && !SrsiStrategyExtensions.KDSellSignal(lastFast, penultFast, srsiSettings)
        && BullishTrend(lastSlow, penultSlow);

    private static bool BullishTrend(SRsiResult lastSlow, SRsiResult penultSlow) =>
        lastSlow.StochK > penultSlow.StochK;

    private static bool BearishTrend(SRsiResult lastSlow, SRsiResult penultSlow) =>
        lastSlow.StochK < penultSlow.StochK;
}

/*
 * 1. Minimal cycles when K is over or under D.
 * 2.
 * */

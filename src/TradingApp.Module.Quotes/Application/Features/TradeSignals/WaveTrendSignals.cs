﻿using TradingApp.Evaluator.Utils;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Enums;

namespace TradingApp.Module.Quotes.Application.Features.TradeSignals;

public static class WaveTrendSignals
{
    private const int DecimalPlace = 4;

    public static TradeAction GetWtSignalsTradeAction(
        IReadOnlyList<Quote> quotes,
        WaveTrendSignal waveTrendResult,
        Minutes maxSignalAge
    )
    {
        return quotes
            .OrderByDescending(q => q.Date)
            .Select(quote => (quotes[^1].Date - quote.Date).TotalMinutes)
            .TakeWhile(signalAgeInMinutes => signalAgeInMinutes <= maxSignalAge.Value)
            .Any()
            ? waveTrendResult.TradeAction
            : TradeAction.Hold;
    }

    public static IReadOnlyList<WaveTrendSignal> CreateWaveTrendSignals(
        IReadOnlyList<WaveTrendResult> waveTrendResults,
        WaveTrendSettings settings
    )
    {
        if (waveTrendResults.Count < 2)
        {
            return new List<WaveTrendSignal>(0);
        }
        var waveTrends = new List<WaveTrendSignal>(waveTrendResults.Count);
        for (var i = 0; i < waveTrendResults.Count; i++)
        {
            if (i == 0)
            {
                waveTrends.Add(new WaveTrendSignal(0, 0, null, TradeAction.Hold));
                continue;
            }
            var currentWt1 = waveTrendResults[i].Wt1;
            var currentWt2 = waveTrendResults[i].Wt2;

            var vwap = currentWt1 - currentWt2;
            waveTrends.Add(
                waveTrendResults[i].Wt1 != 0 && waveTrendResults[i].Wt2 != 0
                    ? new WaveTrendSignal(
                        Math.Round(currentWt1, DecimalPlace),
                        Math.Round(currentWt2, DecimalPlace),
                        MathUtils.RoundValue(vwap, DecimalPlace),
                        GetWtSignalsTradeAction(
                            CrossesDownToUp(waveTrendResults, i, settings),
                            CrossesUpToDown(waveTrendResults, i, settings)
                        )
                    )
                    : new WaveTrendSignal(0, 0, null, TradeAction.Hold)
            );
        }

        return waveTrends;
    }

    /// <summary>
    /// wt1 crosses wt2 from down to up, green buy signal
    /// </summary>
    /// <param name="waveTrendResults"></param>
    /// <param name="i"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    private static bool? CrossesDownToUp(
        IReadOnlyList<WaveTrendResult> waveTrendResults,
        int i,
        WaveTrendSettings settings
    ) =>
        waveTrendResults[i].Wt1 < 0
        && waveTrendResults[i - 1].Wt1 < 0
        && waveTrendResults[i].Wt1 > waveTrendResults[i].Wt2
        && waveTrendResults[i - 1].Wt1 <= waveTrendResults[i - 1].Wt2
        && waveTrendResults[i].Wt1 <= settings.OversoldLevel2
        && waveTrendResults[i].Wt1 >= settings.Oversold
            ? true
            : null;

    /// <summary>
    /// wt1 crosses wt2 from up to down, red sell signal
    /// </summary>
    /// <param name="waveTrendResults"></param>
    /// <param name="i"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    private static bool? CrossesUpToDown(
        IReadOnlyList<WaveTrendResult> waveTrendResults,
        int i,
        WaveTrendSettings settings
    ) =>
        waveTrendResults[i].Wt1 > 0
        && waveTrendResults[i - 1].Wt1 > 0
        && waveTrendResults[i].Wt1 < waveTrendResults[i].Wt2
        && waveTrendResults[i - 1].Wt1 >= waveTrendResults[i - 1].Wt2
        && waveTrendResults[i].Wt1 >= settings.OverboughtLevel2
        && waveTrendResults[i].Wt1 <= settings.Overbought
            ? true
            : null;

    private static TradeAction GetWtSignalsTradeAction(bool? crossesOver, bool? crossesUnder)
    {
        if (crossesOver == true)
        {
            return TradeAction.Buy;
        }
        return crossesUnder == true ? TradeAction.Sell : TradeAction.Hold;
    }
}
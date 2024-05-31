using System.Diagnostics.CodeAnalysis;
using TradingApp.Evaluator.Utils;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Evaluator.Indicators;

[ExcludeFromCodeCoverage]
public static class WaveTrendIndicator
{
    /// <summary>
    /// WaveTrend uses CLH price as source.
    /// Bullish (buy) - When WT1 crosses above WT2,
    /// Bullish (buy) - When Closing price is above VWAP
    /// Bearish (sell) - When WT1 crosses below WT2
    /// Bearish (sell) - When Closing price is below VWAP
    /// hlc3 -  (high + low + close) / 3
    /// ema - Exponential Moving Average
    /// d - difference of hlc3 and ema
    /// ci - composite index , difference of hlv3 and emea
    /// wt1 - wave trend value
    /// wt2 - sma for wave trend value
    /// </summary>
    /// <param name="domainQuotes"></param>
    /// <param name="settings"></param>
    /// <param name="scaleResult"></param>
    /// <param name="resultDecimalPlace"></param>
    /// <returns></returns>
    ///

    public static List<WaveTrendResult?> Calculate(
        IEnumerable<Quote> domainQuotes,
        WaveTrendSettings settings,
        bool scaleResult,
        int resultDecimalPlace
    )
    {
        var hlc3 = domainQuotes.Select(quote => quote.Close + quote.Low + quote.High).ToArray();
        var esa = MovingAverage.CalculateEMA(settings.ChannelLength, hlc3);
        var d = MovingAverage.CalculateEMA(
            settings.ChannelLength,
            hlc3.Select((hlc, i) => Math.Abs(hlc - esa[i])).ToArray()
        );
        var ci = hlc3.Select((close, i) => d[i] != 0 ? (close - esa[i]) / (d[i]) : 0)
            .ToArray();
        var wt1 = MovingAverage.CalculateEMA(settings.AverageLength, ci);
        var wt2 = MovingAverage.CalculateSMA(settings.MovingAverageLength, wt1);

        if (scaleResult)
        {
            var scaleFactor = Scale.ByMaxMin(wt1);
            wt1 = wt1.Select(x => x * scaleFactor).ToArray();
            wt2 = wt2.Select(x => x * scaleFactor).ToArray();
        }

        var crossOvers = CrossesOver(wt1, wt2, settings);
        var crossUnders = CrossesUnder(wt1, wt2, settings);


        return CreateWaveTrendResults(wt1, wt2, crossOvers, crossUnders, resultDecimalPlace);
    }

    private static List<WaveTrendResult?> CreateWaveTrendResults(IReadOnlyList<decimal> wt1, IReadOnlyList<decimal> wt2, IReadOnlyList<bool> crossOvers, IReadOnlyList<bool> crossUnders, int resultDecimalPlace)
    {
        var waveTrends = new List<WaveTrendResult?>();
        for (var i = 0; i < wt1.Count; i++)
        {
            var currentWt1 = wt1[i];
            var currentWt2 = wt2[i];
            var crossesUnder = crossOvers[i];
            var crossesOver = crossUnders[i];

            if (i > 0 && crossOvers[i] && !crossOvers[i - 1])
            {
                currentWt1 = wt1[i - 1];
                currentWt2 = wt2[i - 1];
                crossesOver = false;
            }

            var vwap = currentWt1 - currentWt2;
            waveTrends.Add(currentWt1 != 0 && currentWt2 != 0 ?
                new WaveTrendResult(
                    Math.Round(currentWt1, resultDecimalPlace),
                    Math.Round(currentWt2, resultDecimalPlace),
                    MathUtils.RoundValue(vwap, resultDecimalPlace),
                    crossesOver,
                    crossesUnder
                ) : null
            );
        }

        return waveTrends;
    }


    private static bool[] CrossesOver(IReadOnlyList<decimal> wt1, IReadOnlyList<decimal> wt2, WaveTrendSettings settings)
    {
        var result = new bool[wt1.Count];
        for (var i = 1; i < wt1.Count; i++)
        {
            result[i] = wt1[i] > wt2[i] && wt1[i - 1] <= wt2[i - 1] && decimal.ToDouble(wt1[i]) < settings.OversoldLevel2;
        }
        return result;
    }

    private static bool[] CrossesUnder(IReadOnlyList<decimal> wt1, IReadOnlyList<decimal> wt2, WaveTrendSettings settings)
    {
        var result = new bool[wt1.Count];
        for (var i = 1; i < wt1.Count; i++)
        {
            result[i] = wt1[i] < wt2[i] && wt1[i - 1] >= wt2[i - 1] && decimal.ToDouble(wt1[i]) > settings.OverboughtLevel2;
        }
        return result;
    }
}

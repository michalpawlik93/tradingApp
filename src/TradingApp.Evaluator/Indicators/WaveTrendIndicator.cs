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

    public static IReadOnlyList<WaveTrendResult> Calculate(
        IReadOnlyList<Quote> domainQuotes,
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
        var ci = hlc3.Select((close, i) => d[i] != 0 ? (close - esa[i]) / (d[i]) : 0).ToArray();
        var wt1 = MovingAverage.CalculateEMA(settings.AverageLength, ci);
        var wt2 = MovingAverage.CalculateSMA(settings.MovingAverageLength, wt1);

        if (!scaleResult)
            return CreateResults(wt1, wt2, resultDecimalPlace);
        var scaleFactor = Scale.ByMaxMin(wt1);
        wt1 = wt1.Select(x => x * scaleFactor).ToArray();
        wt2 = wt2.Select(x => x * scaleFactor).ToArray();

        return CreateResults(wt1, wt2, resultDecimalPlace);
    }

    public static List<WaveTrendResult> CreateResults(
        IReadOnlyList<decimal> wt1,
        IReadOnlyList<decimal> wt2,
        int resultDecimalPlace
    )
    {
        var waveTrends = new List<WaveTrendResult>();
        for (var i = 0; i < wt1.Count; i++)
        {
            var currentWt1 = wt1[i];
            var currentWt2 = wt2[i];

            var vwap = currentWt1 - currentWt2;
            waveTrends.Add(
                wt1[i] != 0 && wt2[i] != 0
                    ? new WaveTrendResult(
                        Math.Round(currentWt1, resultDecimalPlace),
                        Math.Round(currentWt2, resultDecimalPlace),
                        MathUtils.RoundValue(vwap, resultDecimalPlace)
                    )
                    : null
            );
        }

        return waveTrends;
    }
}

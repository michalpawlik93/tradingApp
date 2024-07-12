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

    public static IEnumerable<WaveTrendResult> Calculate(
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
        var ci = hlc3.Select((close, i) => d[i] != 0 ? (close - esa[i]) / (d[i]) : 0).ToArray();
        var wt1 = MovingAverage.CalculateEMA(settings.AverageLength, ci);
        var wt2 = MovingAverage.CalculateSMA(settings.MovingAverageLength, wt1);

        if (!scaleResult) return CreateWaveTrendResults(wt1, wt2, resultDecimalPlace, settings);
        var scaleFactor = Scale.ByMaxMin(wt1);
        wt1 = wt1.Select(x => x * scaleFactor).ToArray();
        wt2 = wt2.Select(x => x * scaleFactor).ToArray();

        return CreateWaveTrendResults(wt1, wt2, resultDecimalPlace, settings);
    }

    private static List<WaveTrendResult> CreateWaveTrendResults(
        IReadOnlyList<decimal> wt1,
        IReadOnlyList<decimal> wt2,
        int resultDecimalPlace,
        WaveTrendSettings settings
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
                        MathUtils.RoundValue(vwap, resultDecimalPlace),
                        CrossesDownToUp(wt1, wt2, i, settings),
                        CrossesUpToDown(wt1, wt2, i, settings)
                    )
                    : null
            );
        }

        return waveTrends;
    }

    /// <summary>
    /// wt1 crosses wt2 from down to up, green buy signal
    /// </summary>
    /// <param name="wt1"></param>
    /// <param name="wt2"></param>
    /// <param name="i"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    private static bool? CrossesDownToUp(
        IReadOnlyList<decimal> wt1,
        IReadOnlyList<decimal> wt2,
        int i,
        WaveTrendSettings settings
    ) =>
        wt1[i] < 0
        && wt1[i - 1] < 0
        && wt1[i] > wt2[i]
        && wt1[i - 1] <= wt2[i - 1]
        && decimal.ToDouble(wt1[i]) <= settings.OversoldLevel2
        && decimal.ToDouble(wt1[i]) >= settings.Oversold
            ? true
            : null;

    /// <summary>
    /// wt1 crosses wt2 from up to down, red sell signal
    /// </summary>
    /// <param name="wt1"></param>
    /// <param name="wt2"></param>
    /// <param name="i"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    private static bool? CrossesUpToDown(
        IReadOnlyList<decimal> wt1,
        IReadOnlyList<decimal> wt2,
        int i,
        WaveTrendSettings settings
    ) =>
        wt1[i] > 0
        && wt1[i - 1] > 0
        && wt1[i] < wt2[i]
        && wt1[i - 1] >= wt2[i - 1]
        && decimal.ToDouble(wt1[i]) >= settings.OverboughtLevel2
        && decimal.ToDouble(wt1[i]) <= settings.Overbought
            ? true
            : null;
}

// When wt1 crosses down 0 line,  its bearish
// when wt1 crosses up 0 line its bullish
// when cross over and is above 0 line enter the trade
// vwap when is big then momentum is big. If its under 0 sell, if above 0 buy , price is going up


// nie mozna poelgac na ni  samym. Jak ogolne trend rosnie to wave trend to odzwierciedla
// jak trend ogolny maleje to wt moze dawac false sygnały żeby kupowac

// Buy streategy
// wt crosses over, wt1 is between level lines
// Check adx, adx line crosses above 20 line strong trend is happening

// Ideal for 12h-1day, for low time frame too many signals can be genearated
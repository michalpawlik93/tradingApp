using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Evaluator.Indicators;

public static class StochInidcator
{
    public static IEnumerable<StochResult> Calculate(
        this IEnumerable<Quote> qdList,
        int lookbackPeriods,
        int signalPeriods,
        int smoothPeriods,
        decimal kFactor,
        decimal dFactor,
        MaType movingAverageType
    )
    {
        var length = qdList.Count();
        List<StochResult> results = new(length);

        for (var i = 0; i < length; i++)
        {
            var q = qdList.ElementAt(i);

            StochResult r = new(q.Date);
            results.Add(r);

            if (i + 1 < lookbackPeriods)
                continue;
            var highHigh = decimal.MinValue;
            var lowLow = decimal.MaxValue;

            for (var p = i + 1 - lookbackPeriods; p <= i; p++)
            {
                var x = qdList.ElementAt(p);

                if (x.High > highHigh)
                {
                    highHigh = x.High;
                }

                if (x.Low < lowLow)
                {
                    lowLow = x.Low;
                }
            }

            r.Oscillator = lowLow != highHigh ? 100 * (q.Close - lowLow) / (highHigh - lowLow) : 0;
        }

        if (smoothPeriods > 1)
        {
            results = SmoothOscillator(
                results,
                length,
                lookbackPeriods,
                smoothPeriods,
                movingAverageType
            );
        }

        // handle insufficient length
        if (length < lookbackPeriods - 1)
        {
            return results;
        }

        // signal (%D) and %J
        var signalIndex = lookbackPeriods + smoothPeriods + signalPeriods - 2;

        for (var i = lookbackPeriods - 1; i < length; i++)
        {
            var r = results[i];

            // add signal

            if (signalPeriods <= 1)
            {
                r.Signal = r.Oscillator;
            }
            // SMA case
            else if (i + 1 >= signalIndex && movingAverageType is MaType.SMA)
            {
                decimal? sumOsc = 0;
                for (int p = i + 1 - signalPeriods; p <= i; p++)
                {
                    StochResult x = results[p];
                    sumOsc += x.Oscillator;
                }

                r.Signal = sumOsc / signalPeriods;
            }

            // %J
            r.PercentJ = (kFactor * r.Oscillator) - (dFactor * r.Signal);
        }

        return results;
    }

    private static List<StochResult> SmoothOscillator(
        List<StochResult> results,
        int length,
        int lookbackPeriods,
        int smoothPeriods,
        MaType movingAverageType
    )
    {
        // temporarily store interim smoothed oscillator
        var smooth = new decimal?[length]; // smoothed value

        if (movingAverageType is MaType.SMA)
        {
            var smoothIndex = lookbackPeriods + smoothPeriods - 2;

            for (var i = smoothIndex; i < length; i++)
            {
                decimal? sumOsc = 0;
                for (var p = i + 1 - smoothPeriods; p <= i; p++)
                {
                    sumOsc += results[p].Oscillator;
                }

                smooth[i] = sumOsc / smoothPeriods;
            }
        }

        // replace oscillator
        for (var i = 0; i < length; i++)
        {
            results[i].Oscillator = smooth[i];
        }

        return results;
    }
}

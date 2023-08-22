using TradingApp.Evaluator.Utils;
using TradingApp.Modules.Application.Models;

namespace TradingApp.Evaluator.Indicators;

public static class WaveTrendIndicator
{
    /// <summary>
    /// wt - ema expotentially weighted moving average
    /// wtma - sma movign average
    /// </summary>
    /// <param name="domainQuotes"></param>
    /// <param name="settings"></param>
    /// <param name="scaleResult"></param>
    /// <param name="resultDecimalPlace"></param>
    /// <returns></returns>
    public static List<WaveTrendResult> Calculate(
        IEnumerable<Quote> domainQuotes,
        WaveTrendSettings settings,
        bool scaleResult,
        int resultDecimalPlace
    )
    {
        List<WaveTrendResult> waveTrends = new List<WaveTrendResult>();

        decimal[] src = domainQuotes.Select(quote => quote.Close).ToArray();
        decimal[] esa = MovingAverage.Calculate(settings.ChannelLength, src);
        decimal[] d = MovingAverage.Calculate(
            settings.ChannelLength,
            src.Select((x, i) => Math.Abs(x - esa[i])).ToArray()
        );
        decimal[] ci = src.Select((x, i) => d[i] != 0 ? (x - esa[i]) / (0.015m * d[i]) : 0)
            .ToArray();
        decimal[] tci = MovingAverage.Calculate(settings.AverageLength, ci);
        decimal[] wt = tci;
        decimal[] wtma = MovingAverage.Calculate(settings.MovingAverageLength, wt);

        if (scaleResult)
        {
            decimal scaleFactor = Scale.ByMaxMin(wt);
            wt = wt.Select(x => x * scaleFactor).ToArray();
            wtma = wtma.Select(x => x * scaleFactor).ToArray();
        }

        //  WTO
        bool[] momchangelong = CrossesOver(wt, wtma);
        bool[] momchangeshort = CrossesUnder(wt, wtma);

        for (int i = 0; i < wt.Length; i++)
        {
            decimal? currentWt = wt[i];
            decimal? currentWtma = wtma[i];
            bool crossesUnder = momchangelong[i];
            bool crossesOver = momchangeshort[i];

            if (i > 0 && momchangelong[i] && !momchangelong[i - 1])
            {
                currentWt = wt[i - 1];
                currentWtma = wtma[i - 1];
                crossesOver = false;
            }

            var vwap = currentWt - currentWtma;
            waveTrends.Add(
                new WaveTrendResult(
                    MathUtils.RoundValue(currentWt.Value, resultDecimalPlace),
                    MathUtils.RoundValue(vwap, resultDecimalPlace),
                    crossesOver,
                    crossesUnder
                )
            );
        }
        return waveTrends;
    }

    static bool[] CrossesOver(decimal[] arr1, decimal[] arr2)
    {
        bool[] result = new bool[arr1.Length];
        for (int i = 1; i < arr1.Length; i++)
        {
            result[i] = arr1[i] > arr2[i] && arr1[i - 1] <= arr2[i - 1];
        }
        return result;
    }

    static bool[] CrossesUnder(decimal[] arr1, decimal[] arr2)
    {
        bool[] result = new bool[arr1.Length];
        for (int i = 1; i < arr1.Length; i++)
        {
            result[i] = arr1[i] < arr2[i] && arr1[i - 1] >= arr2[i - 1];
        }
        return result;
    }
}

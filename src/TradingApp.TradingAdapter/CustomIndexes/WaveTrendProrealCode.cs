using TradingApp.TradingAdapter.Models;
using DomainQuote = TradingApp.TradingAdapter.Models.Quote;

namespace TradingApp.TradingAdapter.CustomIndexes
{
    public static class WaveTrendProrealCode
    {
        public static IEnumerable<WaveTrend> GetWaveTrend(IEnumerable<DomainQuote> domainQuotes, WaveTrendSettings settings)
        {

            decimal[] src = domainQuotes.Select(quote => quote.Close).ToArray();
            decimal[] esa = GetMovingAverage(settings.ChannelLength, src);
            decimal[] d = GetMovingAverage(settings.ChannelLength, src.Select((x, i) => Math.Abs(x - esa[i])).ToArray());
            decimal[] ci = src.Select((x, i) => d[i] != 0 ? (x - esa[i]) / (0.015m * d[i]) : 0).ToArray();
            decimal[] tci = GetMovingAverage(settings.AverageLength, ci);
            decimal[] wt1 = tci;
            decimal[] wt1ma = GetMovingAverage(settings.MovingAverageLength, wt1);

            // Scale the results to the range from -100 to 100
            decimal scaleFactor = 100m / wt1.Max(); // Find the scale factor to fit the maximum value to 100
            wt1 = wt1.Select(x => x * scaleFactor).ToArray();
            wt1ma = wt1ma.Select(x => x * scaleFactor).ToArray();

            //  WTO 
            bool[] momchangelong = CrossesOver(wt1, wt1ma);
            bool[] momchangeshort = CrossesUnder(wt1, wt1ma);

            List<WaveTrend> waveTrends = new List<WaveTrend>();
            for (int i = 0; i < wt1.Length; i++)
            {
                decimal? value = wt1[i];
                bool crossesUnder = momchangelong[i];
                bool crossesOver = momchangeshort[i];

                if (i > 0 && momchangelong[i] && !momchangelong[i - 1])
                {
                    value = wt1[i - 1];
                    crossesOver = false;
                }

                waveTrends.Add(new WaveTrend(value, crossesOver, crossesUnder));
            }
            return waveTrends;
        }
        static decimal[] GetMovingAverage(int period, decimal[] input)
        {
            decimal[] result = new decimal[input.Length];
            for (int i = period - 1; i < input.Length; i++)
            {
                result[i] = input.Skip(i - period + 1).Take(period).Average();
            }
            return result;
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
}

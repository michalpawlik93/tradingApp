using DomainQuote = TradingApp.TradingAdapter.Models.Quote;

namespace TradingApp.TradingAdapter.IndexesUtils
{
    public static class WaveTrendCustom
    {
        public static List<decimal> CalculateWaveTrend(List<DomainQuote> ohlcData, List<decimal> smaValues, int channelLength, int averageLength)
        {
            List<decimal> waveTrendValues = new List<decimal>();

            List<decimal> waveTrendDots = new List<decimal>();

            for (int i = averageLength - 1; i < ohlcData.Count; i++)
            {
                decimal highestHigh = decimal.MinValue;
                decimal lowestLow = decimal.MaxValue;

                for (int j = i - channelLength + 1; j <= i; j++)
                {
                    decimal high = ohlcData[j].High;
                    decimal low = ohlcData[j].Low;
                    if (high > highestHigh)
                        highestHigh = high;
                    if (low < lowestLow)
                        lowestLow = low;
                }

                decimal waveTrend = (highestHigh + lowestLow) / 2;
                waveTrendDots.Add(waveTrend);
            }

            int waveTrendDotIndex = 0;

            for (int i = 0; i < ohlcData.Count; i++)
            {
                if (i >= averageLength - 1)
                {
                    decimal waveTrendDot = waveTrendDots[waveTrendDotIndex];
                    decimal smaValue = smaValues[waveTrendDotIndex];
                    decimal waveTrendValue = waveTrendDot - smaValue;
                    waveTrendValues.Add(waveTrendValue);
                    waveTrendDotIndex++;
                }
                else
                {
                    waveTrendValues.Add(0);
                }
            }

            return waveTrendValues;
        }
    }
}

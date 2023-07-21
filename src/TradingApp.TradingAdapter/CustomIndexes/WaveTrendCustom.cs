using Skender.Stock.Indicators;
using TradingApp.Common.Utilities;
using TradingApp.TradingAdapter.Mappers;
using TradingApp.TradingAdapter.Models;
using DomainQuote = TradingApp.TradingAdapter.Models.Quote;

namespace TradingApp.TradingAdapter.IndexesUtils;

public static class WaveTrendCustom
{
    public static IEnumerable<WaveTrend> GetWaveTrend(IEnumerable<DomainQuote> domainQuotes, WaveTrendSettings settings)
    {
        var ohlcData = domainQuotes.ToList();
        var sma = domainQuotes
            .MapToSkenderQuotes()
            .GetSma(settings.AverageLength)
            .Select(r => r.Sma.TryParse()).ToList();
        var waveTrendValues = CalculateWaveTrend(
            ohlcData,
            sma,
            settings.ChannelLength,
            settings.AverageLength
        );

        return ohlcData.Select((q, index) => CreateWaveTrend(waveTrendValues, index));
    }

    private static WaveTrend CreateWaveTrend(List<decimal> waveTrendValues, int index)
    {
        decimal waveTrend = waveTrendValues[index];

        bool isGreenDot = waveTrend > 0;
        bool isRedDot = waveTrend < 0;

        return new WaveTrend(waveTrend, isGreenDot, isRedDot);
    }

    private static List<decimal> CalculateWaveTrend(List<DomainQuote> ohlcData, List<decimal> smaValues, int channelLength, int averageLength)
    {
        List<decimal> waveTrendValues = new List<decimal>();

        List<decimal> waveTrendDots = new List<decimal>();

        if (ohlcData.Count < channelLength)
        {
            // Brak wystarczającej ilości danych wejściowych, ustaw wartość początkową waveTrend na 0 lub inną wartość
            for (int i = 0; i < ohlcData.Count; i++)
            {
                waveTrendValues.Add(0m);
            }

            return waveTrendValues;
        }

        for (int i = channelLength - 1; i < ohlcData.Count; i++)
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

            decimal smaValue = smaValues[i - (channelLength - 1)];
            decimal waveTrendDot = (highestHigh + lowestLow) / 2 - smaValue;
            waveTrendDots.Add(waveTrendDot);
        }

        for (int i = 0; i < ohlcData.Count; i++)
        {
            if (i < channelLength + averageLength - 2)
            {
                waveTrendValues.Add(0m);
            }
            else
            {
                decimal waveTrendSum = 0m;
                int startIndex = i - averageLength + 1;
                int endIndex = i - (channelLength - 1);

                // Sprawdź, czy mamy wystarczającą ilość danych do obliczenia waveTrendDots
                if (startIndex >= 0 && endIndex >= 0 && startIndex < waveTrendDots.Count && endIndex < waveTrendDots.Count)
                {
                    for (int j = startIndex; j <= endIndex; j++)
                    {
                        waveTrendSum += waveTrendDots[j];
                    }
                }

                decimal waveTrendAverage = waveTrendSum / averageLength;
                waveTrendValues.Add(waveTrendAverage);
            }
        }

        return waveTrendValues;
    }
}

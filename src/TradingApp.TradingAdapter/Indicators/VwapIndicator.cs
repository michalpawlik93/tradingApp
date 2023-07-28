using TradingApp.TradingAdapter.Models;

namespace TradingApp.TradingAdapter.Indicators;

public static class VwapIndicator
{
    public static ICollection<VWapResult> Calculate(List<Quote> quotes, int resultDecimalPlace)
    {
        var result = new List<VWapResult>();
        decimal vwap = 0;
        decimal sumVolumePrice = 0;
        decimal sumVolume = 0;
        DateTime? currentDate = null;

        foreach (var currentQuote in quotes)
        {
            if (!currentDate.HasValue || currentQuote.Date.Date != currentDate.Value.Date)
            {
                vwap = 0;
                sumVolumePrice = 0;
                sumVolume = 0;
            }

            if (currentQuote.Volume > 0)
            {
                decimal typicalPrice = (currentQuote.High + currentQuote.Low + currentQuote.Close) / 3;
                sumVolumePrice += currentQuote.Volume * typicalPrice;
                sumVolume += currentQuote.Volume;
                vwap = sumVolumePrice / sumVolume;
            }

            result.Add(new VWapResult() { Value = Math.Round(vwap, resultDecimalPlace) });
            currentDate = currentQuote.Date.Date;
        }

        return result;
    }
}

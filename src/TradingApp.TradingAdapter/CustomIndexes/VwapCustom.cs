using DomainQuote = TradingApp.TradingAdapter.Models.Quote;

namespace TradingApp.TradingAdapter.CustomIndexes;

public static class VwapCustom
{
    public static IEnumerable<decimal?> CalculateVWAP(List<DomainQuote> quotes)
    {
        var result = new List<decimal?>();
        decimal vwap = 0;
        decimal sumVolumePrice = 0;
        decimal sumVolume = 0;
        int d = 0;


        for (int i = 0; i < quotes.Count; i++)
        {
            DomainQuote currentQuote = quotes[i];

            if (i > 0 && currentQuote.Date.Date != quotes[i - 1].Date.Date)
            {
                d = 0;
            }
            else
            {
                d++;
            }

            if (currentQuote.Volume > 0)
            {
                sumVolumePrice += currentQuote.Volume * currentQuote.Close;
                sumVolume += currentQuote.Volume;
                vwap = sumVolumePrice / sumVolume;
            }
            result.Add(CalculateStandardDeviation(quotes, d, vwap));
        }
        return result;
    }

    private static decimal CalculateStandardDeviation(List<DomainQuote> quotes, int d, decimal vwap)
    {
        double sumSquaredDifference = 0;
        decimal sumVolume = 0;

        for (int i = 0; i < quotes.Count; i++)
        {
            DomainQuote currentQuote = quotes[i];
            if (currentQuote.Date.Date == quotes[i - 1].Date.Date && d > 0)
            {
                sumSquaredDifference += Math.Pow((double)(currentQuote.Close - vwap), 2) * (double)currentQuote.Volume;
                sumVolume += currentQuote.Volume;
            }
        }

        return (decimal)Math.Sqrt(sumSquaredDifference / (double)sumVolume);
    }
}
using DomainQuote = TradingApp.TradingAdapter.Models.Quote;

namespace TradingApp.TradingAdapter.CustomIndexes;

public static class VwapCustom
{
    public static List<decimal> CalculateVwap(IEnumerable<DomainQuote> domainQuotes)
    {
        List<decimal> vwapValues = new List<decimal>();

        decimal totalVolumePrice = 0.0m;
        decimal totalVolume = 0.0m;

        foreach (var quote in domainQuotes)
        {
            decimal close = quote.Close;
            decimal volume = quote.Volume;

            totalVolumePrice += close * volume;
            totalVolume += volume;

            if (totalVolume > 0.0m)
            {
                decimal vwap = totalVolumePrice / totalVolume;
                vwapValues.Add(vwap);
            }
            else
            {
                vwapValues.Add(0.0m);
            }
        }

        return vwapValues;
    }
}

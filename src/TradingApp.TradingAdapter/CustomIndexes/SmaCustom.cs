using DomainQuote = TradingApp.TradingAdapter.Models.Quote;

namespace TradingApp.TradingAdapter.CustomIndexes;

public static class SmaCustom
{
    public static List<decimal> CalculateSma(IEnumerable<DomainQuote> domainQuotes, int smaLength)
    {
        var quotesList = domainQuotes.ToList();
        var smaValues = new List<decimal>();

        for (int i = smaLength - 1; i < quotesList.Count; i++)
        {
            decimal sum = 0.0m;
            int startIndex = i - smaLength + 1;
            int endIndex = i;

            for (int j = startIndex; j <= endIndex; j++)
            {
                sum += quotesList[j].Close;
            }

            decimal sma = sum / smaLength;
            smaValues.Add(sma);
        }

        return smaValues;
    }
}

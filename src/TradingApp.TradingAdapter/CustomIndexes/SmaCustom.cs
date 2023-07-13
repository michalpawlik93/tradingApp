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


    private static decimal CalculateSingleSma(IEnumerable<DomainQuote> domainQuotes, int smaLength, int quoteCount)
    {
        if (quoteCount >= smaLength - 1)
        {
            decimal sum = 0.0m;
            int startIndex = quoteCount - smaLength + 1;
            int endIndex = quoteCount;

            for (int i = startIndex; i <= endIndex; i++)
            {
                sum += domainQuotes.ElementAt(i).Close;
            }

            decimal sma = sum / smaLength;
            return sma;
        }
        else
        {
            return 0.0m;
        }
    }

}

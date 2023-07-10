using DomainQuote = TradingApp.TradingAdapter.Models.Quote;

namespace TradingApp.TradingAdapter.CustomIndexes;

public static class SmaCustom
{
    public static List<decimal> CalculateSma(IEnumerable<DomainQuote> domainQuotes, int smaLength) =>
        domainQuotes.Select((q, i) => CalculateSingleSma(domainQuotes, smaLength, i)).ToList();

    private static decimal CalculateSingleSma(IEnumerable<DomainQuote> domainQuotes, int smaLength, int quoteCount)
    {
        if (quoteCount >= smaLength - 1)
        {
            decimal sum = 0.0m;

            for (int i = quoteCount - smaLength + 1; i <= quoteCount; i++)
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

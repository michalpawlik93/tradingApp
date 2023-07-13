using DomainQuote = TradingApp.TradingAdapter.Models.Quote;
using Quote = Skender.Stock.Indicators.Quote;

namespace TradingApp.TradingAdapter.Mappers;

public static class SkenderQuoteMapper
{
    public static IEnumerable<Quote> MapToSkenderQuotes(
        this IEnumerable<DomainQuote> domeinQuotes
    ) =>
        domeinQuotes.Select(
            q =>
                new Quote()
                {
                    Open = q.Open,
                    Close = q.Close,
                    High = q.High,
                    Low = q.Low,
                    Date = q.Date,
                    Volume = q.Volume
                }
        );
}

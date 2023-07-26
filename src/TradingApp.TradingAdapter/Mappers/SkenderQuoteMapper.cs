using Skender.Stock.Indicators;
using TradingApp.TradingAdapter.Models;

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

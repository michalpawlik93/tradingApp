using TradingApp.Core.Utilities;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.TingoProvider.Contract;

namespace TradingApp.TingoProvider.Mappers;

public static class TingoQuoteMapper
{
    public static IEnumerable<Quote> MapToQuotes(this TingoQuote[] tingoQuotes)
    {
        var firstTicker = tingoQuotes.FirstOrDefault();

        return firstTicker != null
            ? firstTicker.PriceData.Select(
                q =>
                    new Quote()
                    {
                        Open = q.Open,
                        High = q.High,
                        Low = q.Low,
                        Close = q.Close,
                        Volume = q.Volume,
                        Date = DateTimeUtils.ConvertUtcIso8601_2DateStringToDateTime(q.Date)
                    }
            )
            : Enumerable.Empty<Quote>();
    }
}

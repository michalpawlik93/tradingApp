using FluentResults;
using TradingApp.Core.Utilities;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.TingoProvider.Contract;

namespace TradingApp.TingoProvider.Mappers;

public static class TingoQuoteMapper
{
    public static Result<IReadOnlyList<Quote>> MapToQuotes(this TingoQuote[] tingoQuotes)
    {
        var firstTicker = tingoQuotes.FirstOrDefault();

        if (firstTicker == null)
        {
            return Result.Ok<IReadOnlyList<Quote>>(new List<Quote>());
        }

        var quotes = new List<Quote>();

        foreach (var q in firstTicker.PriceData)
        {
            var dateResult = DateTimeUtils.ConvertUtcIso8601_2DateStringToDateTime(q.Date);

            if (dateResult.IsFailed)
            {
                return dateResult.ToResult();
            }

            quotes.Add(
                new Quote
                {
                    Open = q.Open,
                    High = q.High,
                    Low = q.Low,
                    Close = q.Close,
                    Volume = q.Volume,
                    Date = dateResult.Value
                }
            );
        }

        return Result.Ok<IReadOnlyList<Quote>>(quotes);
    }
}

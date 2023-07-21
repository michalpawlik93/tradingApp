using TradingApp.TradingAdapter.Models;

namespace TradingApp.Application.Quotes.GetStooqCombinedQuotes.Dto;

public record GetStooqCombinedQuotesResponseDto(
    IEnumerable<CombinedQuote> Quotes,
    RsiSettings RsiSettings
);

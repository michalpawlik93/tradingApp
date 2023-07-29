using TradingApp.TradingAdapter.Models;

namespace TradingApp.Modules.Quotes.GetStooqCombinedQuotes.Dto;

public record GetStooqCombinedQuotesResponseDto(
    IEnumerable<CombinedQuote> Quotes,
    RsiSettings RsiSettings
);

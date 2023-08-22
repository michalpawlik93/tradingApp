using TradingApp.Module.Quotes.Application.Models;

namespace TradingApp.Module.Quotes.Application.GetStooqCombinedQuotes.Dto;

public record GetStooqCombinedQuotesResponseDto(
    IEnumerable<CombinedQuote> Quotes,
    RsiSettings RsiSettings
);

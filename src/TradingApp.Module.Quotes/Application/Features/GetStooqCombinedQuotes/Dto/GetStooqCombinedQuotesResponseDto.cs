using TradingApp.Module.Quotes.Application.Models;

namespace TradingApp.Module.Quotes.Application.Features.GetStooqCombinedQuotes.Dto;

public record GetStooqCombinedQuotesResponseDto(
    IEnumerable<CombinedQuote> Quotes,
    RsiSettings RsiSettings
);

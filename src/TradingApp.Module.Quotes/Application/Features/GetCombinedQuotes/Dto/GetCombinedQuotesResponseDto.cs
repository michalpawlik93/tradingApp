using TradingApp.Module.Quotes.Application.Models;

namespace TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes.Dto;

public record GetCombinedQuotesResponseDto(
    IEnumerable<CombinedQuote> Quotes
);

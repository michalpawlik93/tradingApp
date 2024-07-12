using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes.Dto;

public record GetCombinedQuotesResponseDto(
    IEnumerable<CombinedQuote> Quotes,
    RsiSettings RsiSettings
);

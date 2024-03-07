using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Features.GetStooqCombinedQuotes.Dto;

public record GetStooqCombinedQuotesResponseDto(
    IEnumerable<CombinedQuote> Quotes,
    RsiSettings RsiSettings
);

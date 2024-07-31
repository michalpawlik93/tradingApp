using TradingApp.Module.Quotes.Application.Models;

namespace TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;

public record GetCypherBResponseDto(
    IReadOnlyList<CypherBQuote> Quotes
);

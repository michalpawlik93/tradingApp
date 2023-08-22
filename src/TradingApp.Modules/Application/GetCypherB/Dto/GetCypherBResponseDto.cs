using TradingApp.Modules.Application.Models;

namespace TradingApp.Modules.Application.GetCypherB.Dto;

public record GetCypherBResponseDto(
    IEnumerable<CypherBQuote> Quotes
);

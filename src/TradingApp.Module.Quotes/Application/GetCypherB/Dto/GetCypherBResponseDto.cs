using TradingApp.Module.Quotes.Application.Models;

namespace TradingApp.Module.Quotes.Application.GetCypherB.Dto;

public record GetCypherBResponseDto(
    IEnumerable<CypherBQuote> Quotes
);

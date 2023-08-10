using TradingApp.TradingAdapter.Models;

namespace TradingApp.Modules.Quotes.Application.GetCypherB.Dto;

public record GetCypherBResponseDto(
    IEnumerable<CypherBQuote> Quotes
);

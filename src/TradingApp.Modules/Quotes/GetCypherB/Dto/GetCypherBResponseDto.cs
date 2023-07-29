using TradingApp.TradingAdapter.Models;

namespace TradingApp.Modules.Quotes.GetCypherB.Dto;

public record GetCypherBResponseDto(
    IEnumerable<CypherBQuote> Quotes
);

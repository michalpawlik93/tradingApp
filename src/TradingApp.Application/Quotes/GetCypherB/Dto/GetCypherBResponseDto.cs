using TradingApp.TradingAdapter.Models;

namespace TradingApp.Application.Quotes.GetCypherB.Dto;

public record GetCypherBResponseDto(
    IEnumerable<CypherBQuote> Quotes
);

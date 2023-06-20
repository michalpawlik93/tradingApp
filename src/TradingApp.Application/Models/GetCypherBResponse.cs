using TradingApp.TradingAdapter.Models;

namespace TradingApp.Application.Models;

public record GetCypherBResponse(
    IEnumerable<CypherBQuote> Quotes
);
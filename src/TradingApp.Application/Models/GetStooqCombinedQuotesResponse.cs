using TradingApp.TradingAdapter.Models;

namespace TradingApp.Application.Models;

public record GetStooqCombinedQuotesResponse(IEnumerable<CombinedQuote> Quotes, RsiSettings RsiSettings);

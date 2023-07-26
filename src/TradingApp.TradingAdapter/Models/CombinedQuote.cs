namespace TradingApp.TradingAdapter.Models;

public record CombinedQuote(DomainQuote Ohlc, decimal? Rsi, decimal? Sma);

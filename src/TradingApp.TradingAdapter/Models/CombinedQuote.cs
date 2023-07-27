namespace TradingApp.TradingAdapter.Models;

public record CombinedQuote(Quote Ohlc, decimal? Rsi, decimal? Sma);

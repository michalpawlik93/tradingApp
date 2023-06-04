namespace TradingApp.TradingAdapter.Models;

public record CombinedQuote(Quote Ohlc, double? Rsi, double? Sma);

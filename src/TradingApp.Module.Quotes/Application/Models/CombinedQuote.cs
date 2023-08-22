namespace TradingApp.Module.Quotes.Application.Models;

public record CombinedQuote(Quote Ohlc, decimal? Rsi, decimal? Sma);

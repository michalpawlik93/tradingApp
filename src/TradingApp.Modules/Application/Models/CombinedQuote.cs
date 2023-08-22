namespace TradingApp.Modules.Application.Models;

public record CombinedQuote(Quote Ohlc, decimal? Rsi, decimal? Sma);

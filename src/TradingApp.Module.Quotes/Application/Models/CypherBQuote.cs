namespace TradingApp.Module.Quotes.Application.Models;

public record CypherBQuote(Quote Ohlc, WaveTrendResult WaveTrend, decimal? Mfi, decimal? Vwap);

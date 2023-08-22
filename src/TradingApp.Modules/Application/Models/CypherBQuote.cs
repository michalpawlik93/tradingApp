namespace TradingApp.Modules.Application.Models;

public record CypherBQuote(Quote Ohlc, WaveTrendResult WaveTrend, decimal? Mfi, decimal? Vwap);

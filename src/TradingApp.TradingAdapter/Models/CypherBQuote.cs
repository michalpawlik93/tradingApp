namespace TradingApp.TradingAdapter.Models;

public record CypherBQuote(Quote Ohlc, WaveTrendResult WaveTrend, decimal? Mfi, decimal? Vwap);

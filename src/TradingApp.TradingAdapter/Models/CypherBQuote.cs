namespace TradingApp.TradingAdapter.Models;

public record CypherBQuote(Quote Ohlc, WaveTrend WaveTrend, decimal? Mfi, decimal? Vwap);

namespace TradingApp.TradingAdapter.Models;

public record CypherBQuote(Quote Ohlc, double? MomentumWave, double? Mfi, double? Vwap);

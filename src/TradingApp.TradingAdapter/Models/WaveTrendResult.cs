namespace TradingApp.TradingAdapter.Models;

public record WaveTrendResult(decimal? Value, decimal? Vwap, bool CrossesOver, bool CrossesUnder);

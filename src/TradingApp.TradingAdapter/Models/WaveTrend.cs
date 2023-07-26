namespace TradingApp.TradingAdapter.Models;

public record WaveTrend(decimal? Value, decimal? Vwap, bool CrossesOver, bool CrossesUnder);

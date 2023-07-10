namespace TradingApp.TradingAdapter.Models;

public record WaveTrend(decimal? Value, bool PriceIsMovingLower, bool PriceIsMovingHigher);

namespace TradingApp.TradingAdapter.Models;

public record WaveTrendSettings(RsiSettings RsiSettings, int ChannelLength, int AverageLength, int MovingAverageLength);

namespace TradingApp.TradingAdapter.Models;

public record WaveTrendSettings(double Oversold, double Overbought, int ChannelLength, int AverageLength, int MovingAverageLength);

namespace TradingApp.Modules.Application.Models;

public record WaveTrendSettings(double Oversold, double Overbought, int ChannelLength, int AverageLength, int MovingAverageLength);

namespace TradingApp.Module.Quotes.Application.Models;

public record WaveTrendSettings(double Oversold, double Overbought, int ChannelLength, int AverageLength, int MovingAverageLength);

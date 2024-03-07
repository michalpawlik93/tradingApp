namespace TradingApp.Module.Quotes.Contract.Models;

public record WaveTrendSettings(double Oversold, double Overbought, int ChannelLength, int AverageLength, int MovingAverageLength);

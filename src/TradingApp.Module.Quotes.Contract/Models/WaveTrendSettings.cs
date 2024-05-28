namespace TradingApp.Module.Quotes.Contract.Models;

public record WaveTrendSettings(double Oversold, double Overbought, double OversoldLevel2, double OverboughtLevel2, int ChannelLength, int AverageLength, int MovingAverageLength);

namespace TradingApp.Module.Quotes.Contract.Models;

public record struct WaveTrendSettings(decimal Oversold, decimal Overbought, decimal OversoldLevel2, decimal OverboughtLevel2, int ChannelLength, int AverageLength, int MovingAverageLength);

namespace TradingApp.Module.Quotes.Contract.Models;

public record RsiSettings(double Oversold, double Overbought, bool Enable, int ChannelLength);


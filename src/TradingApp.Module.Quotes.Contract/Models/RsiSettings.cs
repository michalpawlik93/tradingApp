namespace TradingApp.Module.Quotes.Contract.Models;

public record RsiSettings(decimal Oversold, decimal Overbought, bool Enable, int ChannelLength);


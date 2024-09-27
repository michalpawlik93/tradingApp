namespace TradingApp.Module.Quotes.Contract.Models;

public record struct RsiSettings(decimal Oversold, decimal Overbought, bool Enabled, int ChannelLength);


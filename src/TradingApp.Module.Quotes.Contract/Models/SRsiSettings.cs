namespace TradingApp.Module.Quotes.Application.Models;

public record SRsiSettings(bool Enable, int ChannelLength, int StochKSmooth, int StochDSmooth, double Oversold, double Overbought);

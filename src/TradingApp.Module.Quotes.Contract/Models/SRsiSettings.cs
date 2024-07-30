namespace TradingApp.Module.Quotes.Application.Models;

public record struct SRsiSettings(bool Enable, int ChannelLength, int StochKSmooth, int StochDSmooth, decimal Oversold, decimal Overbought);

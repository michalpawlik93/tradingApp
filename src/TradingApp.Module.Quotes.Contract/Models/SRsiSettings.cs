namespace TradingApp.Module.Quotes.Application.Models;

public record SRsiSettings(bool Enable, int Length, int StochKSmooth, int StochDSmooth, double Oversold, double Overbought);

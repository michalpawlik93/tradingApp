namespace TradingApp.Module.Quotes.Application.Models;

public record RsiSettings(double Oversold, double Overbought, bool Enable, int Length);


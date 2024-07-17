namespace TradingApp.Module.Quotes.Contract.Models;

/// <summary>
/// D - Signal
/// K - Oscilator
/// </summary>
public record SRsiResult(DateTime Date, decimal? StochK, decimal? StochD);
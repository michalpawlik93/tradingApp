namespace TradingApp.Module.Quotes.Contract.Models;

/// <summary>
/// D - Signal dashed line
/// K - Oscilator solid line
/// </summary>
public record SRsiResult(DateTime Date, decimal? StochK, decimal? StochD);
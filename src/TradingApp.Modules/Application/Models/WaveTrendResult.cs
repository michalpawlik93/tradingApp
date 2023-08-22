namespace TradingApp.Modules.Application.Models;

public record WaveTrendResult(decimal? Value, decimal? Vwap, bool CrossesOver, bool CrossesUnder);

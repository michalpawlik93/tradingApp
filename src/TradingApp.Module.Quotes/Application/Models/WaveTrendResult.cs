namespace TradingApp.Module.Quotes.Application.Models;

public record WaveTrendResult(decimal? Value, decimal? Vwap, bool CrossesOver, bool CrossesUnder);

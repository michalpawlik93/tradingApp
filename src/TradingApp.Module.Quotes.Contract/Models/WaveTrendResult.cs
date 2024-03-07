namespace TradingApp.Module.Quotes.Contract.Models;

public record WaveTrendResult(decimal? Value, decimal? Vwap, bool CrossesOver, bool CrossesUnder);

namespace TradingApp.TradingAdapter.Models;

public record CypherBQuote(DomainQuote Ohlc, WaveTrend WaveTrend, decimal? Mfi, decimal? Vwap);

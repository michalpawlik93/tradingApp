namespace TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;

public record CypherBQuote(Quote Ohlc, WaveTrendResult WaveTrend, MfiResult Mfi);

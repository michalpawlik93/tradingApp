using TradingApp.Module.Quotes.Domain.Enums;

namespace TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;

public record CypherBQuote(Quote Ohlc, WaveTrendSignalsResult WaveTrendSignalsResult, MfiResult MfiResult);

public record WaveTrendSignalsResult(decimal Wt1, decimal Wt2, decimal? Vwap, TradeAction TradeAction);
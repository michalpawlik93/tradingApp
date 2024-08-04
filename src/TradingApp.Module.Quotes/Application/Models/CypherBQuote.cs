using TradingApp.Module.Quotes.Domain.Enums;

namespace TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;

public record CypherBQuote(Quote Ohlc, WaveTrendSignal WaveTrendSignal, MfiResult MfiResult, SrsiSignal SrsiSignal);

public record WaveTrendSignal(decimal Wt1, decimal Wt2, decimal? Vwap, TradeAction TradeAction);


public record SrsiSignal(decimal StochK, decimal StochD, TradeAction TradeAction);
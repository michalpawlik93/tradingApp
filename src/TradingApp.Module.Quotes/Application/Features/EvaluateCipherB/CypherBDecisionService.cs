using System.Globalization;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;

namespace TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;

public interface ICypherBDecisionService
{
    Decision MakeDecision(IEnumerable<CypherBQuote> quotes);
}


public class CypherBDecisionService : ICypherBDecisionService
{
    public Decision MakeDecision(IEnumerable<CypherBQuote> quotes)
    {
        // CipherB strategy
        var latestQuote = quotes.Last();
        var additionalParams = new Dictionary<string, string>
        {
            { nameof(latestQuote.WaveTrend.Wt2), latestQuote.WaveTrend.Wt2.ToString(CultureInfo.InvariantCulture) },
            { nameof(latestQuote.WaveTrend.Vwap), latestQuote.WaveTrend.Vwap.ToString() },
            { nameof(latestQuote.Mfi.Mfi), latestQuote.Mfi.Mfi.ToString(CultureInfo.InvariantCulture) }
        };
        var decision = Decision.CreateNew(new IndexOutcome("RSI", 0.02M, additionalParams), DateTime.UtcNow, TradeAction.Buy, new SignalStrength(0.02M, SignalStrengthLevel.High), MarketDirection.Bullish);
        return decision;
    }
}


using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;

namespace TradingApp.Module.Quotes.Application.Services;

internal class DecisionService : IDecisionService
{
    public Decision MakeDecision(IndexOutcome indexOutcome)
    {
        return Decision.CreateNew(new IndexOutcome("RSI", 0.02M), DateTime.UtcNow, TradeAction.Buy, new SignalStrength(0.02M, SignalStrengthLevel.High), MarketDirection.Bullish);
    }
}

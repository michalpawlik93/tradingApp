using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.ValueObjects;

namespace TradingApp.Module.Quotes.Application.Services;

public interface IDecisionService
{
    Decision MakeDecision(IndexOutcome indexOutcome);
}

using TradingApp.Core.Domain;
using TradingApp.Module.Quotes.Domain.ValueObjects;

namespace TradingApp.Module.Quotes.Domain.Events.Decision;

public record DecisionCreatedDomainEvent : EventBase
{
    public DecisionId DecisionId { get; }

    public DecisionCreatedDomainEvent(DecisionId decisionId)
    {
        DecisionId = decisionId;
    }
}
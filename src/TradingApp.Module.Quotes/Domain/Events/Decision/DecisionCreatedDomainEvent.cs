using TradingApp.Core.EventBus.Events;
using TradingApp.Module.Quotes.Domain.ValueObjects;

namespace TradingApp.Module.Quotes.Domain.Events.Decision;

public record DecisionCreatedDomainEvent : EventBase, IDomainEvent
{
    public DecisionId DecisionId { get; }

    public DecisionCreatedDomainEvent(DecisionId decisionId)
    {
        DecisionId = decisionId;
    }
}
using TradingApp.Core.Domain;
using TradingApp.Modules.Domain.ValueObjects;

namespace TradingApp.Modules.Domain.Events.Decision;

public record DecisionCreatedDomainEvent : EventBase
{
    public DecisionId DecisionId { get; }

    public DecisionCreatedDomainEvent(DecisionId decisionId)
    {
        DecisionId = decisionId;
    }
}
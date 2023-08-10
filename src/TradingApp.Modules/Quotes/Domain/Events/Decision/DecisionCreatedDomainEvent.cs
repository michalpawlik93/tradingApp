using TradingApp.Core.Domain;
using TradingApp.Modules.Quotes.Domain.ValueObjects;

namespace TradingApp.Modules.Quotes.Domain.Events.Decision;

public record DecisionCreatedDomainEvent : EventBase
{
    public DecisionId DecisionId { get; }

    public DecisionCreatedDomainEvent(DecisionId decisionId)
    {
        DecisionId = decisionId;
    }
}
namespace TradingApp.Core.EventBus.Events;

public record DecisionCreatedIntegrationEvent : EventBase, IIntegrationEvent
{
    public Guid DecisionId { get; }

    public DecisionCreatedIntegrationEvent(Guid decisionId)
    {
        DecisionId = decisionId;
    }
}
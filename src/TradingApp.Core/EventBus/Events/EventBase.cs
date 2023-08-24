namespace TradingApp.Core.EventBus.Events;

public record EventBase : IEvent
{
    public Guid Id { get; }

    public DateTime OccurredOn { get; }

    public EventBase()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
    }
}

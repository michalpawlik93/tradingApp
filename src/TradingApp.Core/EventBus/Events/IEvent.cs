namespace TradingApp.Core.EventBus.Events;

public interface IEvent
{
    Guid Id { get; }

    DateTime OccurredOn { get; }
}

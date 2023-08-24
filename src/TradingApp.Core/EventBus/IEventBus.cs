using TradingApp.Core.Domain;

namespace TradingApp.Core.EventBus;

public interface IEventBus
{
    public Task Publish(IAggregateRoot aggregate, CancellationToken ct);
}

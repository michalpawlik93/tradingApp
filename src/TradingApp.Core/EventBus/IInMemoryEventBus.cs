using TradingApp.Core.Domain;

namespace TradingApp.Core.EventBus;

internal interface IInMemoryEventBus
{
    public Task Publish(IAggregateRoot aggregate, CancellationToken ct);
}

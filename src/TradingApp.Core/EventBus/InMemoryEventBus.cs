using MediatR;
using TradingApp.Core.Domain;

namespace TradingApp.Core.EventBus;

public sealed class InMemoryEventBus : IInMemoryEventBus
{
    private readonly IMediator _mediator;
    public InMemoryEventBus(IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(mediator);
        _mediator = mediator;
    }

    public async Task Publish(IAggregateRoot aggregate, CancellationToken ct)
    {
        var notificationsTasks = aggregate.DomainEvents().Select(e => _mediator.Publish(e, ct));
        await Task.WhenAll(notificationsTasks);
    }
}

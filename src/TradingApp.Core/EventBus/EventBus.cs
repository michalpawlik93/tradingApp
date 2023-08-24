using MassTransit;
using MediatR;
using TradingApp.Core.Domain;

namespace TradingApp.Core.EventBus;

public sealed class EventBus : IEventBus
{
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndPoint;
    public EventBus(IMediator mediator, IPublishEndpoint publishEndPoint)
    {
        ArgumentNullException.ThrowIfNull(mediator);
        ArgumentNullException.ThrowIfNull(publishEndPoint);
        _mediator = mediator;
        _publishEndPoint = publishEndPoint;
    }

    public async Task Publish(IAggregateRoot aggregate, CancellationToken ct)
    {
        var notificationsTasks = aggregate.DomainEvents().Select(e => _mediator.Publish(e, ct));
        await Task.WhenAll(notificationsTasks);
        var integrationEventTasks = aggregate.IntegrationEvents().Select(e => _publishEndPoint.Publish(e, ct));
        await Task.WhenAll(integrationEventTasks);
    }
}

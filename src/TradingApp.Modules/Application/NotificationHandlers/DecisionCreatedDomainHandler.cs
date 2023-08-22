using MediatR;
using TradingApp.Modules.Domain.Events.Decision;

namespace TradingApp.Modules.Application.NotificationHandlers;

public class OrderCreatedHandler : INotificationHandler<DecisionCreatedDomainEvent>
{
    public Task Handle(
        DecisionCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

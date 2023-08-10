using MediatR;
using TradingApp.Modules.Quotes.Domain.Events.Decision;

namespace TradingApp.Modules.Quotes.Application.NotificationHandlers;

public class OrderCreatedHandler : INotificationHandler<DecisionCreatedDomainEvent>
{
    public Task Handle(
        DecisionCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

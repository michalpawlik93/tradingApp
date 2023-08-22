using MediatR;
using TradingApp.Module.Quotes.Domain.Events.Decision;

namespace TradingApp.Module.Quotes.Application.NotificationHandlers;

public class OrderCreatedHandler : INotificationHandler<DecisionCreatedDomainEvent>
{
    public Task Handle(
        DecisionCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

using MediatR;

namespace TradingApp.Core.Domain;

public interface IEvent : INotification
{
    Guid Id { get; }

    DateTime OccurredOn { get; }
}

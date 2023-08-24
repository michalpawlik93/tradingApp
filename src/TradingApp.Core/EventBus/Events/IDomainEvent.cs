using MediatR;

namespace TradingApp.Core.EventBus.Events;

public interface IDomainEvent : IEvent, INotification { }

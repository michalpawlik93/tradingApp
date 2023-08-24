using TradingApp.Core.EventBus.Events;

namespace TradingApp.Core.Domain;

public interface IAggregateRoot
{
    public IReadOnlyCollection<IDomainEvent> DomainEvents();
    public IReadOnlyCollection<IIntegrationEvent> IntegrationEvents();
}

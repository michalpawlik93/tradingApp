namespace TradingApp.Core.Domain;

public interface IAggregateRoot
{
    public IReadOnlyCollection<IEvent> DomainEvents();
}

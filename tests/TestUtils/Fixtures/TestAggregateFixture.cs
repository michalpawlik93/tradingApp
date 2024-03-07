using TradingApp.Core.Domain;
using TradingApp.Core.EventBus.Events;

namespace TestUtils.Fixtures;

public class TestAggregate : IAggregateRoot
{
    public Guid Id => Guid.NewGuid();

    public IReadOnlyCollection<IDomainEvent> DomainEvents()
    {
        return new List<IDomainEvent>() { new TestDomainEvent() };
    }

    public IReadOnlyCollection<IIntegrationEvent> IntegrationEvents()
    {
        return new List<IIntegrationEvent>() { new TestIntegrationEvent() };
    }
}

public class TestIntegrationEvent : TestBaseEvent, IIntegrationEvent { }

public class TestDomainEvent : TestBaseEvent, IDomainEvent { }

public class TestBaseEvent
{
    public Guid Id => Guid.NewGuid();

    public DateTime OccurredOn => DateTime.UtcNow;
}

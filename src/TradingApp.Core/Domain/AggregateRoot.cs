using FluentResults;
using TradingApp.Core.EventBus.Events;

namespace TradingApp.Core.Domain;


public abstract class AggregateRoot<TKey> : IAggregateRoot
{
    public TKey? Id { get; internal set; }

    protected AggregateRoot() { }
    protected AggregateRoot(TKey id)
    {
        Id = id;
    }

    private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
    private List<IIntegrationEvent> _integrationEvents = new List<IIntegrationEvent>();
    public IReadOnlyCollection<IDomainEvent> DomainEvents() => _domainEvents.AsReadOnly();
    public IReadOnlyCollection<IIntegrationEvent> IntegrationEvents() => _integrationEvents.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public virtual Result Validate() => Result.Ok();
    protected void AddDomainEvent(IDomainEvent @event)
    {
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(@event);
    }
    protected void AddIntegrationEvent(IIntegrationEvent @event)
    {
        _integrationEvents ??= new List<IIntegrationEvent>();
        _integrationEvents.Add(@event);
    }

    protected Result CheckRule(IBusinessRule rule)
    {
        return rule.IsBroken() ? Result.Fail(rule.Message) : Result.Ok();
    }
}
//https://github.com/dczerwinskipl/ddd-examples/tree/main/Version1/Accounting/src/Accounting.Domain
//https://github.com/kgrzybek/modular-monolith-with-ddd/tree/master/src/Modules/Meetings/Domain/Meetings
//https://github.com/kgrzybek/modular-monolith-with-ddd/blob/master/src/BuildingBlocks/Domain/TypedIdValueBase.cs
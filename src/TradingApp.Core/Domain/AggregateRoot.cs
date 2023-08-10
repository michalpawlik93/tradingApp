using FluentResults;

namespace TradingApp.Core.Domain;


public abstract class AggregateRoot<TKey> : IAggregateRoot
{
    public TKey? Id { get; internal set; }

    protected AggregateRoot() { }
    protected AggregateRoot(TKey id)
    {
        Id = id;
    }

    private List<IEvent> _domainEvents = new List<IEvent>();
    public IReadOnlyCollection<IEvent> DomainEvents() => _domainEvents.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public virtual Result Validate() => Result.Ok();
    protected void AddDomainEvent(IEvent domainEvent)
    {
        _domainEvents ??= new List<IEvent>();

        _domainEvents.Add(domainEvent);
    }

    protected Result CheckRule(IBusinessRule rule)
    {
        return rule.IsBroken() ? Result.Fail(rule.Message) : Result.Ok();
    }
}
//https://github.com/dczerwinskipl/ddd-examples/tree/main/Version1/Accounting/src/Accounting.Domain
//https://github.com/kgrzybek/modular-monolith-with-ddd/tree/master/src/Modules/Meetings/Domain/Meetings
//https://github.com/kgrzybek/modular-monolith-with-ddd/blob/master/src/BuildingBlocks/Domain/TypedIdValueBase.cs
using System.Diagnostics.CodeAnalysis;

namespace TradingApp.Core.Domain;

public abstract class Entity<TKey>
{
    public required TKey Id { get; init; }

    protected Entity() { }

    [SetsRequiredMembers]
    protected Entity(TKey id)
    {
        Id = id;
    }
}

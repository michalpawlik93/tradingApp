using System.Text.Json.Serialization;

namespace TradingApp.Core.Domain;

public abstract class Identifier<TSelf, TKey> : ValueObject
    where TSelf : Identifier<TSelf, TKey>
    where TKey : notnull
{
    public TKey Id { get; }

    [JsonConstructor]
    protected Identifier(TKey id)
    {
        Id = id;
    }

#pragma warning disable CS8618 // Empty constructor for ORM
    protected Identifier() { }
#pragma warning restore CS8618

    public abstract TSelf Copy();

    public virtual ObjectId ToObjectId() =>
        ObjectId.New(GetType().Name, Id.ToString() ?? string.Empty);

    public override string ToString() => $"({GetType().Name},{Id})";

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}

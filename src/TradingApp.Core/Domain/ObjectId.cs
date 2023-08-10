using System.Text.Json.Serialization;

namespace TradingApp.Core.Domain;

public class ObjectId : ValueObject
{
    public string Type { get; }

    public string Id { get; }

    [JsonConstructor]
    private ObjectId(string type, string id)
    {
        Type = type;
        Id = id;
    }

    public static ObjectId New(string type, string id) => new ObjectId(type, id);

    public ObjectId Copy() => new ObjectId(Id, Type);

    public override string ToString() => $"({Type},{Id})";

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
        yield return Type;
    }
}

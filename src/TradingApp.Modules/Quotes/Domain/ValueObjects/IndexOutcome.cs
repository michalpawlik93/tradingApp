using TradingApp.Core.Domain;

namespace TradingApp.Modules.Quotes.Domain.ValueObjects;

public class IndexOutcome : ValueObject
{
    protected string Name { get; }
    protected decimal Value { get; }
    protected IndexOutcome(string name, decimal value)
    {
        Name = name;
        Value = value;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Value;
    }
}

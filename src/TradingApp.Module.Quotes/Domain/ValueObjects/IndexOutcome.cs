using TradingApp.Core.Domain;

namespace TradingApp.Module.Quotes.Domain.ValueObjects;

public class IndexOutcome : ValueObject
{
    public string Name { get; }
    public decimal? Value { get; }
    public Dictionary<string, string> AdditionalParameters { get; }
    public IndexOutcome(string name, decimal? value = null, Dictionary<string, string> additionalParameters = null)
    {
        Name = name;
        Value = value;
        AdditionalParameters = additionalParameters ?? [];
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Value;
    }
}

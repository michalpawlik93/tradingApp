using TradingApp.Core.Domain;

namespace TradingApp.Module.Quotes.Domain.ValueObjects;

public class IndexOutcome : ValueObject
{
    public string Name { get; }
    public decimal Value { get; }
    public Dictionary<string, string> AdditonalParameters { get; }
    public IndexOutcome(string name, decimal value, Dictionary<string, string> additonalParameters = null)
    {
        Name = name;
        Value = value;
        AdditonalParameters = additonalParameters ?? new Dictionary<string, string>();
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Value;
    }
}

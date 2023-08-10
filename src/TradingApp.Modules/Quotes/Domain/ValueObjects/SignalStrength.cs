using TradingApp.Core.Domain;
using TradingApp.Modules.Quotes.Domain.Enums;

namespace TradingApp.Modules.Quotes.Domain.ValueObjects;

public class SignalStrength : ValueObject
{
    protected decimal Value { get; }
    protected SignalStrengthLevel StrengthLevel { get; }

    protected SignalStrength(decimal value, SignalStrengthLevel strengthLevel)
    {
        Value = value;
        StrengthLevel = strengthLevel;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return StrengthLevel;
    }
}

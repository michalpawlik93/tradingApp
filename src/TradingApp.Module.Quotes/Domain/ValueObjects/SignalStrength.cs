﻿using TradingApp.Core.Domain;
using TradingApp.Module.Quotes.Domain.Enums;

namespace TradingApp.Module.Quotes.Domain.ValueObjects;

public class SignalStrength : ValueObject
{
    public decimal Value { get; }
    public SignalStrengthLevel StrengthLevel { get; }

    public SignalStrength(decimal value, SignalStrengthLevel strengthLevel)
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

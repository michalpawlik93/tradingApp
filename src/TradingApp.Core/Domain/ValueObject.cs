using FluentResults;

namespace TradingApp.Core.Domain;

public abstract class ValueObject
{
    protected static bool EqualOperator(ValueObject? left, ValueObject? right) => !(left is null || right is null) && (ReferenceEquals(left, right) || left.Equals(right));

    protected static bool NotEqualOperator(ValueObject? left, ValueObject? right) => !EqualOperator(left, right);

    protected abstract IEnumerable<object?> GetEqualityComponents();

    public static bool operator ==(ValueObject? one, ValueObject? two) => EqualOperator(one, two);
    public static bool operator !=(ValueObject? one, ValueObject? two) => NotEqualOperator(one, two);

    public virtual Result Validate() => Result.Ok();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    protected Result CheckRule(IBusinessRule rule)
    {
        return rule.IsBroken() ? Result.Fail(rule.Message) : Result.Ok();
    }
}
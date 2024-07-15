using FluentValidation;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace TradingApp.Module.Quotes.Application.Validators;

public static class CommonValidationRules
{
    public static void RuleForNullable<T, TProperty>(
        AbstractValidator<T> validator,
        Expression<Func<T, TProperty>> expression,
        IValidator<TProperty> propertyValidator
    )
    {
        validator
            .RuleFor(expression)
            .NotNull()
            .WithMessage($"Parameter {GetPropertyName(expression)} is mandatory.")
            .SetValidator(propertyValidator);
    }

    public static void RuleForNullable<T, TProperty>(
        AbstractValidator<T> validator,
        Expression<Func<T, TProperty>> expression
    )
    {
        validator
            .RuleFor(expression)
            .NotNull()
            .WithMessage($"Parameter {GetPropertyName(expression)} is mandatory.");
    }

    public static void RuleForEnum<T, TProperty, TEnum>(
        AbstractValidator<T> validator,
        Expression<Func<T, TProperty>> expression
    ) where TEnum : struct, Enum
    {
        var propertyName = GetPropertyName(expression);

        validator
            .RuleFor(expression)
            .NotEmpty()
            .WithMessage($"{propertyName} is required.")
            .Must(value =>
            {
                return value switch
                {
                    string stringValue => Enum.TryParse<TEnum>(stringValue, out _),
                    int intValue => Enum.IsDefined(typeof(TEnum), intValue),
                    _ => false
                };
            })
            .WithMessage(
                $"Parameter {propertyName} must be one of {typeof(TEnum).Name} enumeration."
            );
    }

    public static void RuleForOversold<T>(
        AbstractValidator<T> validator,
        Expression<Func<T, decimal>> expression
    ) =>
        validator
            .RuleFor(expression)
            .GreaterThan(-100)
            .LessThan(0)
            .WithMessage($"{GetPropertyName(expression)} must be between 0 and -100.");

    public static void RuleForOverbought<T>(
        AbstractValidator<T> validator,
        Expression<Func<T, decimal>> expression
    ) =>
        validator
            .RuleFor(expression)
            .ExclusiveBetween(0, 100)
            .WithMessage($"{GetPropertyName(expression)} must be between 0 and 100");

    public static void RuleForChannelLength<T>(
        AbstractValidator<T> validator,
        Expression<Func<T, int>> expression
    ) =>
        validator
            .RuleFor(expression)
            .GreaterThan(0)
            .WithMessage($"{GetPropertyName(expression)}  is required");

    [ExcludeFromCodeCoverage]
    private static string GetPropertyName<T1, T2>(Expression<Func<T1, T2>> expression)
    {
        return expression.Body switch
        {
            MemberExpression memberExpr => memberExpr.Member.Name,
            ParameterExpression paramExpr => paramExpr.Name,
            _
                => throw new ArgumentException(
                    "Expression must be a member expression or a parameter expression with a member expression operand."
                )
        };
    }
}

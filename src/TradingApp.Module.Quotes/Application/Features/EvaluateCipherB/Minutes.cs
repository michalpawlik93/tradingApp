using FluentResults;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Contract.Constants;

namespace TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;

public readonly record struct Minutes
{
    public int Value { get; }

    private Minutes(int value)
    {
        Value = value;
    }

    private static Minutes FromMinutes(int minutes) => new(minutes);

    private static Minutes FromHours(int hours) => new(hours * 60);

    private static Minutes FromDays(int days) => new(days * 60 * 24);

    public static Result<Minutes> GetMaxSignalAge(Granularity granularity) =>
        granularity switch
        {
            Granularity.Daily => Result.Ok(FromDays(5)),
            Granularity.Hourly => Result.Ok(FromHours(5)),
            Granularity.FiveMins => Result.Ok(FromMinutes(5)),
            _
                => Result.Fail<Minutes>(
                    new ValidationError(
                        $"Granularity out of scope. Enum does not exist: {granularity}"
                    )
                )
        };
}

using FluentResults;
using TradingApp.Core.Models;

namespace TradingApp.Evaluator.Utils;

public static class MovingAverage
{
    public static decimal[] CalculateSma(int period, decimal[] values)
    {
        var result = new decimal[values.Length];
        if (values.Length < period)
            return result;

        for (var i = period - 1; i < values.Length; i++)
        {
            result[i] = values.Skip(i - period + 1).Take(period).Average();
        }
        return result;
    }

    public static Result<decimal[]> CalculateEma(int length, decimal[] values)
    {
        var result = new decimal[values.Length];
        if (values.Length < length)
            return Result.Fail(
                new ValidationError(
                    $"{CalculateEma} error. Values length must be bigger than ema length"
                )
            );

        var multiplier = 2m / (length + 1);
        result[length - 1] = values.Take(length).Average();

        for (var i = length; i < values.Length; i++)
        {
            result[i] = (values[i] - result[i - 1]) * multiplier + result[i - 1];
        }
        return Result.Ok(result);
    }
}

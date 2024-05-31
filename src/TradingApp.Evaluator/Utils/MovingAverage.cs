namespace TradingApp.Evaluator.Utils;

public static class MovingAverage
{
    public static decimal[] CalculateSMA(int period, decimal[] input)
    {
        var result = new decimal[input.Length];
        if (input.Length < period)
            return result;

        for (var i = period - 1; i < input.Length; i++)
        {
            result[i] = input.Skip(i - period + 1).Take(period).Average();
        }
        return result;
    }

    public static decimal[] CalculateEMA(int period, decimal[] input)
    {
        var result = new decimal[input.Length];
        if (input.Length < period)
            return result;

        var multiplier = 2m / (period + 1);
        result[period - 1] = input.Take(period).Average();

        for (var i = period; i < input.Length; i++)
        {
            result[i] = (input[i] - result[i - 1]) * multiplier + result[i - 1];
        }
        return result;
    }
}

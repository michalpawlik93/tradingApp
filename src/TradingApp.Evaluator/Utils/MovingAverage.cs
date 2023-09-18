namespace TradingApp.Evaluator.Utils;

public static class MovingAverage
{
    public static decimal[] Calculate(int period, decimal[] input)
    {
        decimal[] result = new decimal[input.Length];
        if (input.Length < period)
            return result;

        for (int i = period - 1; i < input.Length; i++)
        {
            result[i] = input.Skip(i - period + 1).Take(period).Average();
        }
        return result;
    }
}

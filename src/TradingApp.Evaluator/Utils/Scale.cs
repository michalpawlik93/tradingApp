namespace TradingApp.Evaluator.Utils;

public static class Scale
{
    public static decimal ByMaxMin(decimal[] values)
    {
        var maxPositive = values.Where(x => x > 0).DefaultIfEmpty(1).Max();
        var maxNegative = values.Where(x => x < 0).DefaultIfEmpty(1).Min();

        if (maxPositive > Math.Abs(maxNegative))
        {
            return 100m / maxPositive;
        }
        return -100m / maxNegative;
    }
}

namespace TradingApp.TradingAdapter.Utils;

public static class Scale
{
    public static decimal ByMaxMin(decimal[] values)
    {
        decimal maxPositive = values.Where(x => x > 0).DefaultIfEmpty(0).Max();
        decimal maxNegative = values.Where(x => x < 0).DefaultIfEmpty(0).Min();

        if (maxPositive > Math.Abs(maxNegative))
            return 100m / maxPositive;
        else
            return -100m / maxNegative;
    }

}

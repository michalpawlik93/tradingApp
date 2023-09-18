namespace TradingApp.Evaluator.Utils;

public static class MathUtils
{
    public static decimal? RoundValue(decimal? decValue, int resultDecimalPlace) =>
        decValue.HasValue ? Math.Round(decValue.Value, resultDecimalPlace) : null;
}

namespace TradingApp.Core.Utilities;

public static class NumbersUtils
{
    public static decimal? ToNullableDecimal(this double? input) =>
        input.HasValue ? (decimal?)input.Value : null;

    public static decimal TryParse(this double? input) =>
        input.HasValue ? (decimal)input.Value : default;
}

using FluentAssertions;
using TradingApp.TradingAdapter.Utils;

namespace TradingApp.TradingAdapter.Test.Utils;

public class MathUtilsTests
{
    [Theory]
    [InlineData(2.420012, 2, 2.42)]
    [InlineData(null, 4, null)]
    public void RoundValue_ReturnsRoundedValue(
        double? inputValue,
        int decimalPlace,
        double? expectedValue
    )
    {
        // Arrange & Act
        var result = MathUtils.RoundValue((decimal?)inputValue, decimalPlace);

        // Assert
        result.Should().Be((decimal?)expectedValue);
    }
}

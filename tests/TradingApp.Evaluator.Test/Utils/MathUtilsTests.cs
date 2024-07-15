using FluentAssertions;
using TradingApp.Evaluator.Utils;

namespace TradingApp.Evaluator.Test.Utils;

public class MathUtilsTests
{
    [Theory]
    [InlineData(null, 4, null)]
    public void RoundValue_ReturnsRoundedValue(
        decimal? inputValue,
        int decimalPlace,
        decimal? expectedValue
    )
    {
        // Arrange & Act
        var result = MathUtils.RoundValue(inputValue, decimalPlace);

        // Assert
        result.Should().Be(expectedValue);
    }
}

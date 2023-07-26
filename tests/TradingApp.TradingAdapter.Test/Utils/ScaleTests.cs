using FluentAssertions;
using TradingApp.TradingAdapter.Utils;

namespace TradingApp.TradingAdapter.Test.Utils;

public class ScaleTests
{
    [Theory]
    [InlineData(-20, 50, 2)]
    [InlineData(-10, -20, 5)]
    [InlineData(-50, 0, 2)]
    [InlineData(50, 0, 2)]
    public void ByMaxMin_ReturnsScaledFactor(decimal min, decimal max, decimal expected)
    {
        // Arrange
        var inputs = new decimal[] { min, max };
        // Act
        var result = Scale.ByMaxMin(inputs);

        // Assert
        result.Should().Be(expected);
    }
}

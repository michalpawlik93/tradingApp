using FluentAssertions;
using TradingApp.TradingAdapter.Utils;

namespace TradingApp.TradingAdapter.Test.Utils;

public class MovingAverageTests
{
    [Theory]
    [InlineData(20, 30, 2, 0, 25)]
    [InlineData(20, 30, 1, 20, 30)]
    [InlineData(0, 0, 1, 0, 0)]
    public void Calculate_ReturnsScaledFactor(
        decimal first,
        decimal second,
        int period,
        decimal expectedFirst,
        decimal expectedSecond
    )
    {
        // Arrange
        var inputs = new decimal[] { first, second };
        // Act
        var result = MovingAverage.Calculate(period, inputs);

        // Assert
        result[0].Should().Be(expectedFirst);
        result[1].Should().Be(expectedSecond);
    }

    [Fact]
    public void Calculate_ReturnsEmptyResult()
    {
        // Arrange
        var inputs = new decimal[] { };
        // Act
        var result = MovingAverage.Calculate(2, inputs);

        // Assert
        result.Should().BeEmpty();
    }
}

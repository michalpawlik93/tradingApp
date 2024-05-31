using FluentAssertions;
using TradingApp.Evaluator.Utils;

namespace TradingApp.Evaluator.Test.Utils;

public class MovingAverageTests
{
    [Theory]
    [InlineData(20, 30, 2, 0, 25)]
    [InlineData(20, 30, 1, 20, 30)]
    [InlineData(0, 0, 1, 0, 0)]
    public void CalculateSMA_ReturnsScaledFactor(
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
        var result = MovingAverage.CalculateSMA(period, inputs);

        // Assert
        result[0].Should().Be(expectedFirst);
        result[1].Should().Be(expectedSecond);
    }

    [Fact]
    public void CalculateSMA_ReturnsEmptyResult()
    {
        // Arrange
        var inputs = new decimal[] { };
        // Act
        var result = MovingAverage.CalculateSMA(2, inputs);

        // Assert
        result.Should().BeEmpty();
    }

    public static IEnumerable<object[]> TestEmaData()
    {
        yield return [20, 30, 40, 3, new decimal[] { 30 }];
        yield return [20, 30, 40, 3, new decimal[] { 25, 35 }];
        yield return [10, 20, 30, 3, new decimal[] { 20 }];
    }

    [Theory]
    [MemberData(nameof(TestEmaData))]
    public void CalculateEMA_ReturnsExpectedValues(
        decimal first,
        decimal second,
        decimal third,
        int period,
        decimal[] expected
    )
    {
        // Arrange
        var inputs = new decimal[] { first, second, third };

        // Act
        var result = MovingAverage.CalculateEMA(period, inputs);

        // Assert
        for (int i = period - 1; i < expected.Length; i++)
        {
            result[i].Should().BeApproximately(expected[i - (period - 1)], 0.0001m);
        }
    }

    [Fact]
    public void CalculateEMA_ReturnsEmptyResult()
    {
        // Arrange
        var inputs = new decimal[] { };
        // Act
        var result = MovingAverage.CalculateEMA(2, inputs);

        // Assert
        result.Should().BeEmpty();
    }
}

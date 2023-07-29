using FluentAssertions;
using TradingApp.TestUtils;
using TradingApp.TradingAdapter.Indicators;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.TradingAdapter.Test.Indicators;

public class WaveTrendProrealCodeTests : QuotesTestBase
{
    [Fact]
    public void Calculate_Success()
    {
        // Arrange
        var scale = true;
        var decimalPlace = 4;

        // Act
        var results = WaveTrendIndicator.Calculate(quotes.ToList(), TestSettings,
            scale,
            decimalPlace).ToList();

        //Assert
        results.Should().HaveCount(502);

        var r1 = results[13];
        r1.Value.Should().BeApproximately(-51.6591M, 0.0002m);

        var r2 = results[501];
        r2.Value.Should().BeApproximately(-39.1022M, 0.0002m);

        results.MaxBy(x => x.Value)?.Value.Should().BeLessThanOrEqualTo(100);
        results.MinBy(x => x.Value)?.Value.Should().BeGreaterThanOrEqualTo(-100);

        results.MaxBy(x => x.Vwap)?.Value.Should().BeLessThanOrEqualTo(100);
        results.MinBy(x => x.Vwap)?.Value.Should().BeGreaterThanOrEqualTo(-100);
    }

    [Fact]
    public void Calculate_NoQuotes_Success()
    {
        // Arrange
        var scale = false;
        var decimalPlace = 4;

        // Act
        var r0 = WaveTrendIndicator.Calculate(noquotes.ToList(), TestSettings,
            scale,
            decimalPlace).ToList();
        var r1 = WaveTrendIndicator.Calculate(onequote.ToList(), TestSettings,
            scale,
            decimalPlace).ToList();
        // Assert
        r0.Should().BeEmpty();
        r1.Should().HaveCount(1);
    }

    private static WaveTrendSettings TestSettings = new WaveTrendSettings(80, -80, 2, 2, 2);
}

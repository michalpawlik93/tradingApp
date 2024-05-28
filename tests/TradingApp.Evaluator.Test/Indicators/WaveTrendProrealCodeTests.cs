using FluentAssertions;
using TradingApp.Domain.Modules.Constants;
using TradingApp.Evaluator.Indicators;
using TradingApp.TestUtils;

namespace TradingApp.Evaluator.Test.Indicators;

public class WaveTrendProrealCodeTests : QuotesTestBase
{
    [Fact]
    public void Calculate_Success()
    {
        // Arrange
        var scale = true;
        var decimalPlace = 4;

        // Act
        var results = WaveTrendIndicator.Calculate(quotes.ToList(), WaveTrendSettingsConst.WaveTrendSettingsDefault,
            scale,
            decimalPlace).ToList();

        //Assert
        results.Should().HaveCount(502);

        var r1 = results[130];
        r1.Should().NotBeNull();
        r1?.Wt1.Should().BeApproximately(-89.0869M, 0.0002m);

        var r2 = results[501];
        r2.Should().NotBeNull();
        r2?.Wt1.Should().BeApproximately(-37.4638M, 0.0002m);
    }

    [Fact]
    public void Calculate_NoQuotes_Success()
    {
        // Arrange
        var scale = false;
        var decimalPlace = 4;

        // Act
        var r0 = WaveTrendIndicator.Calculate(noquotes.ToList(), WaveTrendSettingsConst.WaveTrendSettingsDefault,
            scale,
            decimalPlace).ToList();
        var r1 = WaveTrendIndicator.Calculate(onequote.ToList(), WaveTrendSettingsConst.WaveTrendSettingsDefault,
            scale,
            decimalPlace).ToList();
        // Assert
        r0.Should().BeEmpty();
        r1.Should().HaveCount(1);
    }
}

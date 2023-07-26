using FluentAssertions;
using TradingApp.TestUtils.Fixtures;
using TradingApp.TradingAdapter.CustomIndexes;

namespace TradingApp.TradingAdapter.Test.CustomIndexes;

public class VwapCustomTests
{
    [Fact]
    public void CalculateVWAP_ScaleValuesDisabled_ReturnsCorrectVWAPValues()
    {
        // Arrange
        var testData = OhlcFixtures.BtcOhlcQuotes();

        // Act
        var vwapValues = VwapCustom.CalculateVWAP(testData, 4).ToList();

        // Assert
        vwapValues.Should().HaveCount(testData.Count);
        vwapValues.Should().Equal(23818.4267M, 23808.8769M, 23791.8441M, 23762.1910M);
    }
}

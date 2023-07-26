using FluentAssertions;
using TradingApp.TestUtils.Fixtures;
using TradingApp.TradingAdapter.CustomIndexes;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.TradingAdapter.Test.CustomIndexes
{
    public class WaveTrendProrealCodeTests
    {
        [Fact]
        public void GetWaveTrend_ShouldReturnWaveTrendWithScaledResult_WhenScaleIsTrue()
        {
            // Arrange
            var testData = OhlcFixtures.BtcOhlcQuotes();
            bool scaleResult = true;

            // Act
            var waveTrends = WaveTrendProrealCode.GetWaveTrend(
                testData,
                TestSettings,
                scaleResult,
                4
            );

            // Assert
            waveTrends[0].Value.Should().Be(0M);
            waveTrends[1].Value.Should().Be(-0.0391M);
            waveTrends[2].Value.Should().Be(-40.4163M);
            waveTrends[3].Value.Should().Be(-100.0000M);
        }

        [Fact]
        public void GetWaveTrend_ShouldReturnWaveTrendWithUnscaledResult_WhenScaleIsFalse()
        {
            // Arrange
            var testData = OhlcFixtures.BtcOhlcQuotes();
            bool scaleResult = false;

            // Act
            var waveTrends = WaveTrendProrealCode
                .GetWaveTrend(testData, TestSettings, scaleResult, 4)
                .ToList();

            // Assert
            waveTrends[0].Value.Should().Be(0M);
            waveTrends[1].Value.Should().Be(-0.0362M);
            waveTrends[2].Value.Should().Be(-37.3731M);
            waveTrends[3].Value.Should().Be(-92.4704M);
        }

        private static WaveTrendSettings TestSettings = new WaveTrendSettings(
            new RsiSettings(-80, 80),
            2,
            2,
            2
        );
    }
}

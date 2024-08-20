using FluentAssertions;
using System.Globalization;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Constants;
using TradingApp.Module.Quotes.Evaluator.Indicators;
using TradingApp.TestUtils;

namespace TradingApp.Module.Quotes.Evaluator.Test.Indicators
{
    public class MoneyFlowIndicatorTests : QuotesTestBase
    {
        readonly CultureInfo _cultureInfo = new("en-US");

        [Fact]
        public void Calculate_Success()
        {
            // Arrange
            var settings = new MfiSettings(20, MfiSettingsConst.ScaleFactor);

            // Act
            var results = MoneyFlowIndicator.Calculate(quotes.ToList(), settings, true, 4);

            // Assert
            Assert.NotNull(results);
            Assert.Equal(quotes.Count(), results.Count());
        }

        [Fact]
        public void Calculate_With_Custom_Quotes()
        {
            // Arrange
            var customQuotes = new List<Quote>
            {
                new(DateTime.Parse("2024-05-01", _cultureInfo), 100, 110, 90, 105, 10000),
                new(DateTime.Parse("2024-05-02", _cultureInfo), 105, 115, 95, 110, 12000),
                new(DateTime.Parse("2024-05-03", _cultureInfo), 110, 120, 100, 115, 8000),
                new(DateTime.Parse("2024-05-04", _cultureInfo), 115, 125, 105, 120, 7000),
                new(DateTime.Parse("2024-05-05", _cultureInfo), 120, 130, 110, 125, 6000),
                new(DateTime.Parse("2024-05-06", _cultureInfo), 125, 135, 115, 130, 5000),
                new(DateTime.Parse("2024-05-07", _cultureInfo), 130, 140, 120, 135, 4000),
                new(DateTime.Parse("2024-05-08", _cultureInfo), 135, 145, 125, 140, 3000),
                new(DateTime.Parse("2024-05-09", _cultureInfo), 140, 150, 130, 145, 2000),
                DivideByNullScenario,
            };

            // Act
            var results = MoneyFlowIndicator.Calculate(
                customQuotes,
                new MfiSettings(10, MfiSettingsConst.ScaleFactor),
                true,
                4
            );

            // Assert
            results.Should().HaveCount(customQuotes.Count);
            // Add more assertions as needed to test specific scenarios
        }

        private static Quote DivideByNullScenario =>
            new(DateTime.Parse("2024-05-10", new CultureInfo("en-US")), 145, 155, 155, 150, 1000);
    }
}

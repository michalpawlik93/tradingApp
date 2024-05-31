using FluentAssertions;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Constants;
using TradingApp.Module.Quotes.Evaluator.Indicators;
using TradingApp.TestUtils;

namespace TradingApp.Module.Quotes.Evaluator.Test.Indicators
{
    public class MoneyFlowIndicatorTests : QuotesTestBase
    {
        [Fact]
        public void Calculate_Success()
        {
            // Arrange
            var settings = new MfiSettings(20, MfiSettingsConst.ScaleFactor);

            // Act
            var results = MoneyFlowIndicator.Calculate(quotes, settings, true, 4);

            // Assert
            Assert.NotNull(results);
            Assert.Equal(quotes.Count(), results.Count);
        }

        [Fact]
        public void Calculate_With_Custom_Quotes()
        {
            // Arrange
            var customQuotes = new List<Quote>
            {
                new Quote(DateTime.Parse("2024-05-01"), 100, 110, 90, 105, 10000),  // High money inflow
                new Quote(DateTime.Parse("2024-05-02"), 105, 115, 95, 110, 12000),  // High money inflow
                new Quote(DateTime.Parse("2024-05-03"), 110, 120, 100, 115, 8000),  // High money inflow
                new Quote(DateTime.Parse("2024-05-04"), 115, 125, 105, 120, 7000),  // High money inflow
                new Quote(DateTime.Parse("2024-05-05"), 120, 130, 110, 125, 6000),  // High money inflow
                new Quote(DateTime.Parse("2024-05-06"), 125, 135, 115, 130, 5000),  // High money inflow
                new Quote(DateTime.Parse("2024-05-07"), 130, 140, 120, 135, 4000),  // High money inflow
                new Quote(DateTime.Parse("2024-05-08"), 135, 145, 125, 140, 3000),  // High money inflow
                new Quote(DateTime.Parse("2024-05-09"), 140, 150, 130, 145, 2000),  // High money inflow
                new Quote(DateTime.Parse("2024-05-10"), 145, 155, 135, 150, 1000),  // High money inflow
            };

            // Act
            var results = MoneyFlowIndicator.Calculate(customQuotes, new MfiSettings(10, MfiSettingsConst.ScaleFactor), true, 4);

            // Assert
            results.Should().HaveCount(customQuotes.Count);
            // Add more assertions as needed to test specific scenarios
        }
    }
}

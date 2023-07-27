using TradingApp.TradingAdapter.Enums;
using TradingApp.TradingAdapter.Models;
using TradingApp.TradingAdapter.Utils;

namespace TradingApp.TradingAdapter.Test.Utils;

public class TimeFrameFilterTests
{
    [Fact]
    public void FilterByTimeFrame_ShouldFilterQuotesWithinTimeFrame()
    {
        // Arrange
        var quotes = new List<Quote>
        {
            new Quote(new DateTime(2023, 7, 20), 1,2,3,4,5),
            new Quote(new DateTime(2024, 7, 20), 1,2,3,4,5),
                        new Quote(new DateTime(2025, 7, 20), 1,2,3,4,5),
        };

        var timeFrame = new TimeFrame(Granularity.Daily, new DateTime(2024, 7, 20), new DateTime(2025, 7, 20));

        // Act
        var filteredQuotes = quotes.FilterByTimeFrame(timeFrame);

        // Assert
        Assert.Collection(filteredQuotes,
            quote => Assert.Equal(new DateTime(2024, 7, 20), quote.Date),
            quote => Assert.Equal(new DateTime(2025, 7, 20), quote.Date)
        );
    }
}

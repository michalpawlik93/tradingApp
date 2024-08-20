using TradingApp.Module.Quotes.Application.Utils;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Evaluator.Test.Utils;

public class TimeFrameFilterTests
{

    [Fact]
    public void FilterByTimeFrame_ShouldFilterQuotesWithinTimeFrame()
    {
        // Arrange
        var quotes = new List<Quote>
        {
            new(new DateTime(2023, 7, 20, 0, 0, 0, DateTimeKind.Utc), 1, 2, 3, 4, 5),
            new(new DateTime(2024, 7, 20, 0, 0, 0, DateTimeKind.Utc), 1, 2, 3, 4, 5),
            new(new DateTime(2025, 7, 20, 0, 0, 0, DateTimeKind.Utc), 1, 2, 3, 4, 5),
        };

        var timeFrame = new TimeFrame(
            Granularity.Daily,
            new DateTime(2024, 7, 20, 0, 0, 0, DateTimeKind.Utc),
            new DateTime(2025, 7, 20, 0, 0, 0, DateTimeKind.Utc)
        );

        // Act
        var filteredQuotes = quotes.FilterByTimeFrame(timeFrame);

        // Assert
        Assert.Collection(
            filteredQuotes,
            quote => Assert.Equal(new DateTime(2024, 7, 20, 0, 0, 0, DateTimeKind.Utc), quote.Date),
            quote => Assert.Equal(new DateTime(2025, 7, 20, 0, 0, 0, DateTimeKind.Utc), quote.Date)
        );
    }
}

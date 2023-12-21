using TradingApp.Core.Utilities;

namespace TradingApp.Core.Tests.Utilities;

public class DateTimeUtilsTests
{
    [Fact]
    public void ParseIso8601DateString_NullDateString_ReturnsNull()
    {
        // Arrange
        string dateString = null!;

        // Act
        var result = DateTimeUtils.ParseIso8601DateString(dateString);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ParseIso8601DateString_InvalidDateString_ThrowsArgumentException()
    {
        // Arrange
        string dateString = "invalid-date";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => DateTimeUtils.ParseIso8601DateString(dateString));
    }

    [Fact]
    public void ParseIso8601DateString_ValidDateString_ReturnsCorrectDateTime()
    {
        // Arrange
        string dateString = "2023-07-12T10:30:00.000Z";
        var expectedDateTime = new DateTime(2023, 07, 12, 10, 30, 0, DateTimeKind.Utc);

        // Act
        var result = DateTimeUtils.ParseIso8601DateString(dateString);

        // Assert
        Assert.Equal(expectedDateTime, result);
    }

    [Fact]
    public void ParseDateTime_ShouldReturnParsedDateTime_WhenBothDateAndTimeAreValid()
    {
        // Arrange
        string dateInput = "20220101";
        string timeInput = "123456";

        // Act
        DateTime result = DateTimeUtils.ParseDateTime(dateInput, timeInput);

        // Assert
        Assert.Equal(new DateTime(2022, 1, 1, 12, 34, 56), result);
    }

    [Fact]
    public void ParseDateTime_ShouldReturnParsedDate_WhenOnlyDateIsValid()
    {
        // Arrange
        string dateInput = "20220101";
        string timeInput = "invalid";

        // Act
        DateTime result = DateTimeUtils.ParseDateTime(dateInput, timeInput);

        // Assert
        Assert.Equal(new DateTime(2022, 1, 1), result);
    }

    [Fact]
    public void ParseDateTime_ShouldReturnParsedTime_WhenOnlyTimeIsValid()
    {
        // Arrange
        string dateInput = "invalid";
        string timeInput = "123456";

        // Act
        DateTime result = DateTimeUtils.ParseDateTime(dateInput, timeInput);

        // Assert
        Assert.Equal(DateTime.MinValue.TimeOfDay.Add(new TimeSpan(12, 34, 56)), result.TimeOfDay);
    }
}

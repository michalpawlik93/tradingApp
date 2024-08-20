using TradingApp.Core.Utilities;

namespace TradingApp.Core.Tests.Utilities;

public class DateTimeUtilsTests
{
    [Fact]
    public void ConvertUtcIso8601_2DateStringToDateTime_ValidDateString_ReturnsCorrectDateTime()
    {
        // Arrange
        var dateString = "2023-07-12T10:30:00+01:00";
        var expectedDateTime = new DateTime(2023, 07, 12, 09, 30, 0, DateTimeKind.Utc);

        // Act
        var result = DateTimeUtils.ConvertUtcIso8601_2DateStringToDateTime(dateString);

        // Assert
        Assert.Equal(expectedDateTime, result);
    }

    [Fact]
    public void ConvertIso8601_1DateStringToDateTime_InvalidDateString_ThrowsArgumentException()
    {
        // Arrange
        var dateString = "invalid-date";

        // Act & Assert
        Assert.Throws<ArgumentException>(
            () => DateTimeUtils.ConvertIso8601_1DateStringToDateTime(dateString)
        );
    }

    [Fact]
    public void ConvertIso8601_1DateStringToDateTime_ValidDateString_ReturnsCorrectDateTime()
    {
        // Arrange
        var dateString = "2023-07-12T10:30:00.000Z";
        var expectedDateTime = new DateTime(2023, 07, 12, 10, 30, 0, DateTimeKind.Utc);

        // Act
        var result = DateTimeUtils.ConvertIso8601_1DateStringToDateTime(dateString);

        // Assert
        Assert.Equal(expectedDateTime, result);
    }

    [Fact]
    public void ParseDateTime_ShouldReturnParsedDateTime_WhenBothDateAndTimeAreValid()
    {
        // Arrange
        var dateInput = "20220101";
        var timeInput = "123456";

        // Act
        var result = DateTimeUtils.ParseDateTime(dateInput, timeInput);

        // Assert
        Assert.Equal(new DateTime(2022, 1, 1, 12, 34, 56, DateTimeKind.Utc), result);
    }

    [Fact]
    public void ParseDateTime_ShouldReturnParsedDate_WhenOnlyDateIsValid()
    {
        // Arrange
        var dateInput = "20220101";
        var timeInput = "invalid";

        // Act
        var result = DateTimeUtils.ParseDateTime(dateInput, timeInput);

        // Assert
        Assert.Equal(new DateTime(2022, 1, 1, 0, 0, 0, DateTimeKind.Utc), result);
    }

    [Fact]
    public void ParseDateTime_ShouldReturnParsedTime_WhenOnlyTimeIsValid()
    {
        // Arrange
        var dateInput = "invalid";
        var timeInput = "123456";

        // Act
        var result = DateTimeUtils.ParseDateTime(dateInput, timeInput);

        // Assert
        Assert.Equal(DateTime.MinValue.TimeOfDay.Add(new TimeSpan(12, 34, 56)), result.TimeOfDay);
    }
}

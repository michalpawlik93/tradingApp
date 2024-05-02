using System.Globalization;

namespace TradingApp.Core.Utilities;

public static class DateTimeUtils
{
    private const string ISO8601_1 = "yyyy-MM-ddTHH:mm:ss.fffZ";
    private const string ISO8601_2 = "yyyy-MM-ddTHH:mm:sszzz";
    /// <summary>
    /// Parse local date time string to UTC DateTime
    /// </summary>
    /// <param name="dateString"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static DateTime ConvertIso8601_1DateStringToDateTime(string dateString) =>
        DateTime.SpecifyKind(ParseIso8601(dateString, ISO8601_1), DateTimeKind.Utc);

    /// <summary>
    /// Parse UTC date time string to UTC DateTime
    /// </summary>
    /// <param name="dateString"></param>
    /// <returns></returns>
    /// example: 1997-07-16T19:20:30+01:00
    /// <exception cref="ArgumentException"></exception>
    public static DateTime ConvertUtcIso8601_2DateStringToDateTime(string dateString) => ParseIso8601(dateString, ISO8601_2);

    public static string ConvertDateTimeToIso8601_2String(DateTime dateTime) =>
        dateTime.ToString(ISO8601_2);

    private static DateTime ParseIso8601(string dateString, string format) =>
        DateTime.TryParseExact(
            dateString,
            format,
            CultureInfo.InvariantCulture,
            DateTimeStyles.AdjustToUniversal,
            out var parsedDate
        )
            ? parsedDate
            : throw new ArgumentException("Invalid ISO 8601 date string.", nameof(dateString));

    public static DateTime ParseDateTime(string dateInput, string timeInput)
    {
        var dateParsed = DateTime.TryParseExact(
            dateInput,
            "yyyyMMdd",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var parsedDate
        );
        var timeParsed = DateTime.TryParseExact(
            timeInput,
            "HHmmss",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var parsedTime
        );
        if (dateParsed && timeParsed)
        {
            return parsedDate.Date + parsedTime.TimeOfDay;
        }

        if (dateParsed)
        {
            return parsedDate.Date;
        }

        if (timeParsed)
        {
            return parsedTime;
        }

        return DateTime.MinValue;
    }
}

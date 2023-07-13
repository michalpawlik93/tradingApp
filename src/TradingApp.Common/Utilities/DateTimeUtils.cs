using System.Globalization;

namespace TradingApp.Common.Utilities;

public static class DateTimeUtils
{
    public static DateTime? ParseIso8601DateString(string? dateString)
    {
        if (string.IsNullOrEmpty(dateString))
        {
            return null;
        }

        if (DateTime.TryParseExact(dateString, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var parsedDate))
        {
            return DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);
        }

        throw new ArgumentException("Invalid ISO 8601 date string.", nameof(dateString));
    }

    public static DateTime ParseDateTime(string dateInput, string timeInput)
    {
        var dateParsed = DateTime.TryParseExact(dateInput, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate);
        var timeParsed = DateTime.TryParseExact(timeInput, "HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTime);
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

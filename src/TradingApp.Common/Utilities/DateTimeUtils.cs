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
}

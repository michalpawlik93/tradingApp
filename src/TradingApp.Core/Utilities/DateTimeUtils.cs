using FluentResults;
using System.Globalization;
using TradingApp.Core.Models;

namespace TradingApp.Core.Utilities;

public static class DateTimeUtils
{
    private const string Iso86011 = "yyyy-MM-ddTHH:mm:ss.fffZ";
    private const string Iso86012 = "yyyy-MM-ddTHH:mm:sszzz";

    /// <summary>
    /// Parse local date time string to UTC DateTime
    /// </summary>
    /// <param name="dateString"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static Result<DateTime> ConvertIso8601_1DateStringToDateTime(string dateString)
    {
        var parseResult = ParseIso8601(dateString, Iso86011);

        if (!parseResult.IsSuccess)
            return parseResult;
        var specifiedDate = DateTime.SpecifyKind(parseResult.Value, DateTimeKind.Utc);
        return Result.Ok(specifiedDate);
    }

    /// <summary>
    /// Parse UTC date time string to UTC DateTime
    /// </summary>
    /// <param name="dateString"></param>
    /// <returns></returns>
    /// example: 1997-07-16T19:20:30+01:00
    /// <exception cref="ArgumentException"></exception>
    public static Result<DateTime> ConvertUtcIso8601_2DateStringToDateTime(string dateString) =>
        ParseIso8601(dateString, Iso86012);

    public static string ConvertDateTimeToIso8601_2String(DateTime dateTime) =>
        dateTime.ToString(Iso86012);

    private static Result<DateTime> ParseIso8601(string dateString, string format) =>
        DateTime.TryParseExact(
            dateString,
            format,
            CultureInfo.InvariantCulture,
            DateTimeStyles.AdjustToUniversal,
            out var parsedDate
        )
            ? Result.Ok(parsedDate)
            : Result.Fail(new ValidationError("Invalid ISO 8601 date string."));

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
        switch (dateParsed)
        {
            case true when timeParsed:
                return parsedDate.Date + parsedTime.TimeOfDay;
            case true:
                return parsedDate.Date;
        }

        return timeParsed ? parsedTime : DateTime.MinValue;
    }
}

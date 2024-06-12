using FluentResults;
using System.Diagnostics.CodeAnalysis;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.TestUtils.Importer;

[ExcludeFromCodeCoverage]
public static class CsvImporter
{
    public static Quote QuoteFromCsv(string csvLine)
    {
        if (string.IsNullOrEmpty(csvLine))
        {
            return new Quote();
        }

        var values = csvLine.Split(',');

        var date = ToDateTime(values[0]);
        var open = ToDecimal(values[1], "open");
        var high = ToDecimal(values[2], "high");
        var low = ToDecimal(values[3], "low");
        var close = ToDecimal(values[4], "close");
        var volume = ToDecimal(values[5], "volume");
        return new Quote(date.Value, open.Value, high.Value, low.Value, close.Value, volume.Value);
    }

    private static Result<DateTime> ToDateTime(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Fail<DateTime>(new SystemError("Missing value in csv file"));
        }

        return DateTime.TryParse(value, out DateTime dateTime)
            ? Result.Ok(dateTime)
            : Result.Fail<DateTime>(new SystemError("Can not parse date time"));
    }

    private static Result<decimal> ToDecimal(string value, string name)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Fail<decimal>(new SystemError($"Missing decimal value in csv file {name}"));
        }

        return decimal.TryParse(value, out var result)
            ? Result.Ok(result)
            : Result.Fail<decimal>(new SystemError("Can not parse to decimal"));
    }
}

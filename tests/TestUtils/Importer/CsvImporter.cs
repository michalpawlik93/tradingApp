using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.TestUtils.Importer;

[ExcludeFromCodeCoverage]
public static class CsvImporter
{
    private static readonly CultureInfo EnglishCulture = new("en-US", false);

    public static Quote QuoteFromCsv(string csvLine)
    {
        if (string.IsNullOrEmpty(csvLine))
        {
            return new Quote();
        }

        var values = csvLine.Split(',');
        var date = values[0] == "" ? default : Convert.ToDateTime(values[0], EnglishCulture);
        var open = ToDecimal(values[1]);
        var high = ToDecimal(values[2]);
        var low = ToDecimal(values[3]);
        var close = ToDecimal(values[4]);
        var volume = ToDecimal(values[5]);
        return new Quote(date, open, high, low, close, volume);
    }

    private static decimal ToDecimal(string value) =>
        value == "" ? default : Convert.ToDecimal(value, EnglishCulture);
}

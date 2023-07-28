using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using TradingApp.TradingAdapter.Models;

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

        string[] values = csvLine.Split(',');
        Quote quote = new();

        HandleOHLCV(quote, "D", values[0]);
        HandleOHLCV(quote, "O", values[1]);
        HandleOHLCV(quote, "H", values[2]);
        HandleOHLCV(quote, "L", values[3]);
        HandleOHLCV(quote, "C", values[4]);
        HandleOHLCV(quote, "V", values[5]);

        return quote;
    }

    private static void HandleOHLCV(Quote quote, string position, string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        switch (position)
        {
            case "D":
                quote.Date = Convert.ToDateTime(value, EnglishCulture);
                break;
            case "O":
                quote.Open = Convert.ToDecimal(value, EnglishCulture);
                break;
            case "H":
                quote.High = Convert.ToDecimal(value, EnglishCulture);
                break;
            case "L":
                quote.Low = Convert.ToDecimal(value, EnglishCulture);
                break;
            case "C":
                quote.Close = Convert.ToDecimal(value, EnglishCulture);
                break;
            case "V":
                quote.Volume = Convert.ToDecimal(value, EnglishCulture);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(position));
        }
    }
}

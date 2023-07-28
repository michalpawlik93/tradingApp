using System.Diagnostics.CodeAnalysis;
using TradingApp.TestUtils.Importer;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.TestUtils.Fixtures;

[ExcludeFromCodeCoverage]
public static class QuotesFixture
{
    public static IEnumerable<Quote> GetDefault(int days = 502)
    {
        return File.ReadAllLines(GetDataTesFilePath("default.csv"))
                   .Skip(1)
                   .Select(CsvImporter.QuoteFromCsv)
                   .OrderByDescending(x => x.Date)
                   .Take(days)
                   .ToList();
    }

    internal static IEnumerable<Quote> GetBitcoin(int days = 1246) =>
        File.ReadAllLines(GetDataTesFilePath("bitcoin.csv"))
            .Skip(1)
            .Select(CsvImporter.QuoteFromCsv)
            .OrderByDescending(x => x.Date)
            .Take(days)
            .ToList();

    private static string GetDataTesFilePath(string file)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        var testsFolderName = "tests";
        int index = currentDirectory.IndexOf(testsFolderName, StringComparison.OrdinalIgnoreCase);
        string basePath = currentDirectory.Substring(0, index + testsFolderName.Length);
        return Path.Combine(basePath, file);
    }
}

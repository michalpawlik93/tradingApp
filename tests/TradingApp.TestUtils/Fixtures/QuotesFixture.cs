using TradingApp.TestUtils.Importer;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.TestUtils.Fixtures;

public static class QuotesFixture
{
    public static IEnumerable<Quote> GetDefault(int days = 502) =>
        File.ReadAllLines("Data/default.csv")
            .Skip(1)
            .Select(CsvImporter.QuoteFromCsv)
            .OrderByDescending(x => x.Date)
            .Take(days)
            .ToList();

    internal static IEnumerable<Quote> GetBitcoin(int days = 1246) =>
        File.ReadAllLines("Data/bitcoin.csv")
            .Skip(1)
            .Select(CsvImporter.QuoteFromCsv)
            .OrderByDescending(x => x.Date)
            .Take(days)
            .ToList();
}

using System.Diagnostics.CodeAnalysis;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.TestUtils.Fixtures;

[ExcludeFromCodeCoverage]
public static class OhlcFixtures
{
    public static List<Quote> BtcOhlcQuotes()
    {
        return new List<Quote>
        {
            new Quote(StartDate, 23785.58M, 23854.03M, 23777.66M, 23823.59M, 17666.24144965M),
            new Quote(StartDate.AddHours(1), 23825.07M, 23836.24M, 23765.03M, 23797.73M, 18317.456283414M),
            new Quote(StartDate.AddHours(2), 23797.73M, 23818.66M, 23719.7M, 23764.81M, 25409.795687338M),
            new Quote(StartDate.AddHours(3), 23765.83M, 23941.88M, 23607.44M, 23607.44M, 42072.132671081M)
        };
    }

    private static DateTime StartDate = new(2023, 1, 1, 1, 1, 1);
}

using System.Diagnostics.CodeAnalysis;
using TestUtils.Fixtures;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.TestUtils.Fixtures;

namespace TradingApp.TestUtils;

[ExcludeFromCodeCoverage]
public abstract class QuotesTestBase
{
    public static readonly IEnumerable<Quote> quotes = QuotesFixture.GetDefault().OrderBy(x => x.Date);
    public static readonly IEnumerable<Quote> noquotes = new List<Quote>();
    public static readonly IEnumerable<Quote> onequote = QuotesFixture.GetDefault(1);
    public static readonly IEnumerable<Quote> btcQuotes = QuotesFixture.GetBitcoin();
    public static readonly IEnumerable<Quote> badQuotes = QuotesFixture.GetBad();
    // RANDOM: gaussian brownaian motion
    public static IEnumerable<Quote> GetRandom(int days = 502)
        => new RandomGbm(bars: days);
}

using System.Globalization;
using TradingApp.TestUtils.Fixtures;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.TestUtils;

public abstract class QuotesTestBase
{
    public static readonly CultureInfo EnglishCulture = new("en-US", false);
    public static readonly IEnumerable<Quote> quotes = QuotesFixture.GetDefault();
    public static readonly IEnumerable<Quote> noquotes = new List<Quote>();
    public static readonly IEnumerable<Quote> onequote = QuotesFixture.GetDefault(1);
    public static readonly IEnumerable<Quote> btcQuotes = QuotesFixture.GetBitcoin();
}

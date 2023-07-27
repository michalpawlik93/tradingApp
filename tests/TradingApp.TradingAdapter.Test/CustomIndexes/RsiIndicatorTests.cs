using FluentAssertions;
using TradingApp.TestUtils;
using TradingApp.TradingAdapter.Indicators;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.TradingAdapter.Test.CustomIndexes;

public class RsiIndicatorTests : QuotesTestBase
{
    [Fact]
    public void Standard()
    {
        var results = RsiIndicator.Calculate(quotes.ToList(), Settings).ToList();

        // proper quantities
        results.Should().HaveCount(502);
        results.Count(x => x.Value != null).Should().Be(488);

        // sample values
        Rsi r1 = results[13];
        Assert.Null(r1.Value);

        Rsi r2 = results[14];
        r2.Value.Should().BeApproximately(62.0541m, 0.0001m);

        Rsi r3 = results[249];
        r3.Value.Should().BeApproximately(70.9368m, 0.0001m);

        Rsi r4 = results[501];
        r4.Value.Should().BeApproximately(42.0773m, 0.0001m);
    }

    [Fact]
    public void SmallLength()
    {
        var results = RsiIndicator.Calculate(quotes.ToList(), new RsiSettings(1, 1, true, 1)).ToList();

        // proper quantities
        Assert.Equal(502, results.Count);
        results.Count(x => x.Value != null).Should().Be(501);

        // sample values
        Rsi r1 = results[28];
        Assert.Equal(100, r1.Value);

        Rsi r2 = results[52];
        Assert.Equal(0, r2.Value);
    }

    [Fact]
    public void CryptoData()
    {
        var results = RsiIndicator.Calculate(btcQuotes.ToList(), Settings).ToList();
        results.Should().HaveCount(1246);
    }


    [Fact]
    public void NoQuotes()
    {
        var r0 = RsiIndicator.Calculate(noquotes.ToList(), Settings).ToList();

        r0.Should().BeEmpty();

        var r1 = RsiIndicator.Calculate(onequote.ToList(), Settings).ToList();

        r1.Should().HaveCount(1);
    }

    [Fact]
    public void Exceptions()
    {
        Action action = () => RsiIndicator.Calculate(noquotes.ToList(), Settings);
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    private readonly RsiSettings Settings = new RsiSettings(1, 1, true, 14);
}

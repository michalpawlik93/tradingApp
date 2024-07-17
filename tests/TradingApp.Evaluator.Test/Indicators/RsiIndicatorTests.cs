using FluentAssertions;
using TradingApp.Evaluator.Indicators;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.TestUtils;

namespace TradingApp.Evaluator.Test.Indicators;

public class RsiIndicatorTests : QuotesTestBase
{
    [Fact]
    public void Calculate_Success()
    {
        // Arrange & Act
        var results = RsiIndicator.Calculate(quotes.ToList(), Settings).ToList();

        //Assert
        results.Should().HaveCount(502);
        results.Count(x => x.Value != null).Should().Be(488);

        RsiResult r1 = results[13];
        Assert.Null(r1.Value);

        RsiResult r2 = results[14];
        r2.Value.Should().BeApproximately(62.0540M, 0.0002m);

        RsiResult r3 = results[501];
        r3.Value.Should().BeApproximately(42.0773m, 0.0002m);
    }

    [Fact]
    public void Calculate_SmallQuotes_Success()
    {
        // Arrange & Act
        var results = RsiIndicator.Calculate(quotes.ToList(), new RsiSettings(1, 1, true, 1)).ToList();

        //Assert
        Assert.Equal(502, results.Count);
        results.Count(x => x.Value != null).Should().Be(501);

        RsiResult r1 = results[28];
        Assert.Equal(100, r1.Value);

        RsiResult r2 = results[52];
        Assert.Equal(0, r2.Value);
    }

    [Fact]
    public void Calculate_Crypto_Success()
    {
        // Arrange & Act
        var results = RsiIndicator.Calculate(btcQuotes.ToList(), Settings).ToList();
        //Assert
        results.Should().HaveCount(1246);
    }


    [Fact]
    public void Calculate_NoQuotes_Success()
    {
        // Arrange & Act
        var r0 = RsiIndicator.Calculate(noquotes.ToList(), Settings).ToList();
        var r1 = RsiIndicator.Calculate(onequote.ToList(), Settings).ToList();
        //Assert
        r0.Should().BeEmpty();
        r1.Should().HaveCount(1);
    }

    [Fact]
    public void Calculate_Length0_ThrowsException()
    {
        // Arrange & Act
        Action action = () => RsiIndicator.Calculate(noquotes.ToList(), new RsiSettings(1, 1, true, 0));
        //Assert
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    private readonly RsiSettings Settings = new RsiSettings(1, 1, true, 14);
}

using FluentAssertions;
using TradingApp.Evaluator.Indicators;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.TestUtils;

namespace TradingApp.Evaluator.Test.Indicators;

public class StochRsiTests : QuotesTestBase
{

    [Fact]
    public void FastRsi()
    {
        //Arrange
        int stochPeriods = 14;
        int dSmoot = 3;
        int kSmooth = 1;
        //Act
        List<SRsiResult> results = SRsiIndicator.Calculate(quotes.ToList(), new SRsiSettings(true, stochPeriods, kSmooth, dSmoot, -60, 60)).ToList();

        //Assert
        results.Should().HaveCount(502);
        results.Count(x => x.StochK != null).Should().Be(475);
        results.Count(x => x.StochD != null).Should().Be(473);

        var r1 = results[31];
        r1.StochK.Should().BeApproximately(32.7366M, 0.001M);
        r1.StochD.Should().BeApproximately(32.9992M, 0.001M);

        var r2 = results[152];
        r2.StochK.Should().BeApproximately(60.3232M, 0.001M);
        r2.StochD.Should().BeApproximately(52.7899M, 0.001M);

        var r3 = results[249];
        r3.StochK.Should().Be(0);
        r3.StochD.Should().Be(0);

        var r4 = results[501];
        r4.StochK.Should().BeApproximately(49.4026M, 0.001M);
        r4.StochD.Should().BeApproximately(70.3125M, 0.001M);
    }

    [Fact]
    public void SlowRsi()
    {
        // Arrange
        int stochPeriods = 14;
        int dSmoot = 3;
        int kSmooth = 3;

        // Act
        List<SRsiResult> results = SRsiIndicator.Calculate(quotes.ToList(), new SRsiSettings(true, stochPeriods, kSmooth, dSmoot, -60, 60)).ToList();

        // Assert
        results.Should().HaveCount(502);
        results.Count(x => x.StochK != null).Should().Be(473);
        results.Count(x => x.StochD != null).Should().Be(471);

        var r1 = results[31];
        r1.StochK.Should().BeApproximately(32.9992M, 0.0001M);
        r1.StochD.Should().BeApproximately(36.4794M, 0.0001M);

        var r2 = results[152];
        r2.StochK.Should().BeApproximately(52.7899M, 0.0001M);
        r2.StochD.Should().BeApproximately(36.8465M, 0.0001M);

        var r3 = results[249];
        r3.StochK.Should().Be(0);
        r3.StochD.Should().BeApproximately(0.4395M, 0.0001M);

        var r4 = results[501];
        r4.StochK.Should().BeApproximately(70.3125M, 0.0001M);
        r4.StochD.Should().BeApproximately(81.0675M, 0.0001M);
    }


    [Fact]
    public void NoQuotes()
    {
        var r0 = SRsiIndicator.Calculate(noquotes.ToList(), new SRsiSettings(true, 12, 3, 3, -60, 60)).ToList();
        var r1 = SRsiIndicator.Calculate(onequote.ToList(), new SRsiSettings(true, 12, 3, 3, -60, 60)).ToList();

        r0.Should().BeEmpty();
        r1.Should().HaveCount(1);
    }

    [Fact]
    public void Removed()
    {
        // Arrange
        int stochPeriods = 14;
        int dSmoot = 3;
        int kSmooth = 3;

        List<SRsiResult> results = SRsiIndicator.Calculate(quotes.ToList(), new SRsiSettings(true, stochPeriods, kSmooth, dSmoot, -60, 60)).ToList();

        // Act
        int removeQty = stochPeriods + stochPeriods + kSmooth + 100;
        List<SRsiResult> removedResults = results.SkipLast(removeQty).ToList();

        // Assert
        removedResults.Should().HaveCount(502 - removeQty);

        SRsiResult last = removedResults.LastOrDefault();
        last.StochK.Should().BeApproximately(0.7494M, 0.0001M);
        last.StochD.Should().BeApproximately(0.7494M, 0.0001M);
    }

    [Fact]
    public void Exceptions()
    {
        // bad RSI period
        Action action1 = () => SRsiIndicator.Calculate(noquotes.ToList(), new SRsiSettings(true, 0, 14, 3, -60, 60));
        Action action2 = () => SRsiIndicator.Calculate(noquotes.ToList(), new SRsiSettings(true, 14, 0, 3, -60, 60));
        Action action3 = () => SRsiIndicator.Calculate(noquotes.ToList(), new SRsiSettings(true, 14, 14, 0, -60, 60));


        //Assert
        action1.Should().Throw<ArgumentOutOfRangeException>();
        action2.Should().Throw<ArgumentOutOfRangeException>();
        action3.Should().Throw<ArgumentOutOfRangeException>();
    }
}

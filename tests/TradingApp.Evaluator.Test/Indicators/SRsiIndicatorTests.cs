using FluentAssertions;
using TradingApp.Evaluator.Indicators;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.TestUtils;

namespace TradingApp.Evaluator.Test.Indicators;

public class StochRsiTests : QuotesTestBase
{
    [Fact]
    public void FastRsi()
    {
        //Arrange
        var stochPeriods = 14;
        var dSmoot = 3;
        var kSmooth = 1;
        //Act
        var results = SRsiIndicator.Calculate(quotes.ToList(), new SRsiSettings(true, stochPeriods, kSmooth, dSmoot, -60, 60)).ToList();

        //Assert
        results.Should().HaveCount(502);
        results.Count(x => x.StochK != null).Should().Be(475);
        results.Count(x => x.StochD != null).Should().Be(473);

        var r1 = results[31];
        r1.StochK.Should().BeApproximately(93.3333M, 0.001M);
        r1.StochD.Should().BeApproximately(97.7778M, 0.001M);

        var r2 = results[152];
        r2.StochK.Should().Be(0);
        r2.StochD.Should().Be(0);

        var r3 = results[249];
        r3.StochK.Should().BeApproximately(36.5517M, 0.001M);
        r3.StochD.Should().BeApproximately(27.3094M, 0.001M);

        var r4 = results[501];
        r4.StochK.Should().BeApproximately(97.5244M, 0.001M);
        r4.StochD.Should().BeApproximately(89.8385M, 0.001M);
    }

    [Fact]
    public void SlowRsi()
    {
        // Arrange
        var stochPeriods = 14;
        var dSmoot = 3;
        var kSmooth = 3;

        // Act
        var results = SRsiIndicator.Calculate(quotes.ToList(), new SRsiSettings(true, stochPeriods, kSmooth, dSmoot, -60, 60)).ToList();

        // Assert
        results.Should().HaveCount(502);
        results.Count(x => x.StochK != null).Should().Be(473);
        results.Count(x => x.StochD != null).Should().Be(471);

        var r1 = results[31];
        r1.StochK.Should().BeApproximately(97.7778M, 0.0001M);
        r1.StochD.Should().BeApproximately(99.2593M, 0.0001M);

        var r2 = results[152];
        r2.StochK.Should().Be(0);
        r2.StochD.Should().BeApproximately(20.0263M, 0.0001M);

        var r3 = results[249];
        r3.StochK.Should().BeApproximately(27.3094M, 0.0001M);
        r3.StochD.Should().BeApproximately(33.2716M, 0.0001M);

        var r4 = results[501];
        r4.StochK.Should().BeApproximately(89.8385M, 0.0001M);
        r4.StochD.Should().BeApproximately(73.4176M, 0.0001M);

    }


    [Fact]
    public void NoQuotes()
    {
        var r0 = SRsiIndicator.Calculate(noquotes.ToList(), new SRsiSettings(true, 12, 3, 3, -60, 60)).ToList();
        var r1 = SRsiIndicator.Calculate(onequote.ToList(), new SRsiSettings(true, 12, 3, 3, -60, 60)).ToList();

        r0.Should().BeEmpty();
        r1.Should().HaveCount(1);
    }
}

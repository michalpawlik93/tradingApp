using FluentAssertions;
using TradingApp.Evaluator.Indicators;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.TestUtils;

namespace TradingApp.Evaluator.Test.Indicators;

public class StochInidcatorTests : QuotesTestBase
{
    [Fact]
    public void Standard()
    {
        // Arrange
        var lookbackPeriods = 14;
        var signalPeriods = 3;
        var smoothPeriods = 3;
        decimal kFactor = 3;
        decimal dFactor = 2;

        // Act
        var results = quotes
            .Calculate(lookbackPeriods, signalPeriods, smoothPeriods, kFactor, dFactor, MaType.SMA)
            .ToList();

        // Assert
        results.Should().HaveCount(502);
        results.Count(x => x.Oscillator != null).Should().Be(487);
        results.Count(x => x.Signal != null).Should().Be(485);

        var r15 = results[15];
        r15.Oscillator.Should().BeApproximately(81.1253M, 0.0001M);
        r15.Signal.Should().BeNull();
        r15.PercentJ.Should().BeNull();

        var r17 = results[17];
        r17.Oscillator.Should().BeApproximately(92.1307M, 0.0001M);
        r17.Signal.Should().BeApproximately(88.4995M, 0.0001M);
        r17.PercentJ.Should().BeApproximately(99.3929M, 0.0001M);

        var r149 = results[149];
        r149.Oscillator.Should().BeApproximately(81.6870M, 0.0001M);
        r149.Signal.Should().BeApproximately(79.7935M, 0.0001M);
        r149.PercentJ.Should().BeApproximately(85.4741M, 0.0001M);

        var r249 = results[249];
        r249.Oscillator.Should().BeApproximately(83.2020M, 0.0001M);
        r249.Signal.Should().BeApproximately(83.0813M, 0.0001M);
        r249.PercentJ.Should().BeApproximately(83.4435M, 0.0001M);

        var r501 = results[501];
        r501.Oscillator.Should().BeApproximately(43.1353M, 0.0001M);
        r501.Signal.Should().BeApproximately(35.5674M, 0.0001M);
        r501.PercentJ.Should().BeApproximately(58.2712M, 0.0001M);
    }

    [Fact]
    public void NoSignal()
    {
        // Arrange
        var lookbackPeriods = 5;
        var signalPeriods = 1;
        var smoothPeriods = 3;

        //Act
        var results = quotes
            .Calculate(lookbackPeriods, signalPeriods, smoothPeriods, 3, 2, MaType.SMA)
            .ToList();

        // Assert
        var r1 = results[487];
        r1.Oscillator.Should().Be(r1.Signal);

        var r2 = results[501];
        r2.Oscillator.Should().Be(r2.Signal);
    }

    [Fact]
    public void Fast()
    {
        // Arrange
        var lookbackPeriods = 5;
        var signalPeriods = 10;
        var smoothPeriods = 1;

        // Act
        var results = quotes
            .Calculate(lookbackPeriods, signalPeriods, smoothPeriods, 3, 2, MaType.SMA)
            .ToList();

        // Assert
        var r1 = results[487];
        r1.Oscillator.Should().BeApproximately(25.0352M, 0.0001M);
        r1.Signal.Should().BeApproximately(60.5706M, 0.0001M);

        var r2 = results[501];
        r2.Oscillator.Should().BeApproximately(91.6233M, 0.0001M);
        r2.Signal.Should().BeApproximately(36.0608M, 0.0001M);
    }

    //something wrong
    [Fact]
    public void FastSmall()
    {
        // Arrange
        var lookbackPeriods = 1;
        var signalPeriods = 10;
        var smoothPeriods = 1;

        // Act
        var results = quotes
            .Calculate(lookbackPeriods, signalPeriods, smoothPeriods, 3, 2, MaType.SMA)
            .ToList();

        // Assert
        var r1 = results[70];
        r1.Oscillator.Should().BeApproximately(0M, 0.0001M);

        var r2 = results[71];
        r2.Oscillator.Should().BeApproximately(100M, 0.0001M);
    }

    [Fact]
    public void BadData()
    {
        var r = badQuotes.Calculate(15, 3, 3, 3, 2, MaType.SMA).ToList();
        r.Should().HaveCount(502);
    }

    [Fact]
    public void NoQuotes()
    {
        // Arrange
        var lookbackPeriods = 1;
        var signalPeriods = 10;
        var smoothPeriods = 1;

        // Act
        var r0 = noquotes
            .Calculate(lookbackPeriods, signalPeriods, smoothPeriods, 3, 2, MaType.SMA)
            .ToList();

        // Assert
        r0.Should().HaveCount(0);

        var r1 = onequote
            .Calculate(lookbackPeriods, signalPeriods, smoothPeriods, 3, 2, MaType.SMA)
            .ToList();

        r1.Should().HaveCount(1);
    }

    [Fact]
    public void Boundary()
    {
        var lookbackPeriods = 14;
        var signalPeriods = 3;
        var smoothPeriods = 3;

        var results = GetRandom(2500)
            .Calculate(lookbackPeriods, signalPeriods, smoothPeriods, 3, 2, MaType.SMA)
            .ToList();

        foreach (var r in results)
        {
            if (r.Oscillator is not null)
            {
                r.Oscillator.Should().BeGreaterOrEqualTo(0);
                r.Oscillator.Should().BeLessOrEqualTo(100);
            }

            if (r.Signal is not null)
            {
                r.Signal.Should().BeGreaterOrEqualTo(0);
                r.Signal.Should().BeLessOrEqualTo(100);
            }

            if (r.PercentJ is not null)
            {
                r.Signal.Should().BeGreaterOrEqualTo(0);
                r.Signal.Should().BeLessOrEqualTo(100);
            }
        }
    }
}

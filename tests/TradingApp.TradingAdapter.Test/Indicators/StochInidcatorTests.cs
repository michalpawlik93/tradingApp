using FluentAssertions;
using TradingApp.TestUtils;
using TradingApp.TradingAdapter.Enums;
using TradingApp.TradingAdapter.Indicators;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.TradingAdapter.Test.Indicators;

public class StochInidcatorTests : QuotesTestBase
{
    [Fact]
    public void Standard()
    {
        // Arrange
        int lookbackPeriods = 14;
        int signalPeriods = 3;
        int smoothPeriods = 3;
        decimal kFactor = 3;
        decimal dFactor = 2;

        // Act
        var results = quotes.ToList().Calculate(lookbackPeriods, signalPeriods, smoothPeriods, kFactor, dFactor, MaType.SMA).ToList();

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
        int lookbackPeriods = 5;
        int signalPeriods = 1;
        int smoothPeriods = 3;

        //Act
        var results = quotes.ToList().Calculate(lookbackPeriods, signalPeriods, smoothPeriods, 3, 2, MaType.SMA).ToList();

        // Assert
        StochResult r1 = results[487];
        r1.Oscillator.Should().Be(r1.Signal);

        StochResult r2 = results[501];
        r2.Oscillator.Should().Be(r2.Signal);
    }

    [Fact]
    public void Fast()
    {
        // Arrange
        int lookbackPeriods = 5;
        int signalPeriods = 10;
        int smoothPeriods = 1;

        // Act
        var results = quotes.ToList().Calculate(lookbackPeriods, signalPeriods, smoothPeriods, 3, 2, MaType.SMA).ToList();

        // Assert
        var r1 = results[487];
        r1.Oscillator.Should().BeApproximately(25.0353M, 0.0001M);
        r1.Signal.Should().BeApproximately(60.5706M, 0.0001M);

        var r2 = results[501];
        r2.Oscillator.Should().BeApproximately(91.6233M, 0.0001M);
        r2.Signal.Should().BeApproximately(36.0608M, 0.0001M);
    }

    [Fact]
    public void FastSmall()
    {
        // Arrange
        int lookbackPeriods = 1;
        int signalPeriods = 10;
        int smoothPeriods = 1;

        // Act
        var results = quotes.ToList().Calculate(lookbackPeriods, signalPeriods, smoothPeriods, 3, 2, MaType.SMA).ToList();

        // Assert
        var r1 = results[70];
        r1.Oscillator.Should().Be(0);

        var r2 = results[71];
        r2.Oscillator.Should().Be(100);
    }

    [Fact]
    public void NoQuotes()
    {
        // Arrange
        int lookbackPeriods = 1;
        int signalPeriods = 10;
        int smoothPeriods = 1;

        // Act
        List<StochResult> r0 = noquotes.ToList().Calculate(lookbackPeriods, signalPeriods, smoothPeriods, 3, 2, MaType.SMA).ToList();

        // Assert
        r0.Should().HaveCount(0);

        List<StochResult> r1 = onequote.ToList().Calculate(lookbackPeriods, signalPeriods, smoothPeriods, 3, 2, MaType.SMA).ToList();

        r1.Should().HaveCount(1);
    }

    [Fact]
    public void Exceptions()
    {
        // bad RSI period
        Action action1 = () => quotes.ToList().Calculate(0, 3, 3, 3, 2, MaType.SMA).ToList();
        Action action2 = () => quotes.ToList().Calculate(14, 0, 3, 3, 2, MaType.SMA).ToList();
        Action action3 = () => quotes.ToList().Calculate(14, 3, 0, 3, 2, MaType.SMA).ToList();
        Action action4 = () => quotes.ToList().Calculate(14, 3, 0, 3, 0, MaType.SMA).ToList();
        Action action5 = () => quotes.ToList().Calculate(14, 3, 0, 3, 2, MaType.SMA).ToList();


        //Assert
        action1.Should().Throw<ArgumentOutOfRangeException>();
        action2.Should().Throw<ArgumentOutOfRangeException>();
        action3.Should().Throw<ArgumentOutOfRangeException>();
        action4.Should().Throw<ArgumentOutOfRangeException>();
        action5.Should().Throw<ArgumentOutOfRangeException>();
    }
}

using FluentAssertions;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Application.Features.TradeSignals;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Enums;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.TradeSignals;

public class WaveTrendSignalsTests
{
    [Fact]
    public void GetWtSignalsTradeAction_HoldExpected()
    {
        // Arrange
        var now = DateTime.Now;
        var tooOldSignal = new Quote(now.AddMinutes(-6), 1m, 2m, 3m, 4m, 5m);
        var withingSignalAge = new Quote(now.AddMinutes(-5), 1m, 2m, 3m, 4m, 5m);
        var quotes = new List<Quote> { tooOldSignal, withingSignalAge };

        // Act
        var result = WaveTrendSignals.GetWtSignalsTradeAction(
            quotes,
            new WaveTrendSignal(1m, 2m, 3m, TradeAction.Hold),
            Minutes.GetMaxSignalAge(Granularity.FiveMins).Value
        );

        // Assert
        result.Should().Be(TradeAction.Hold);
    }

    [Fact]
    public void GetWtSignalsTradeAction_BuyExpected()
    {
        // Arrange
        var now = DateTime.Now;
        var withingSignalAge1 = new Quote(now.AddMinutes(-4), 1m, 2m, 3m, 4m, 5m);
        var withingSignalAge2 = new Quote(now.AddMinutes(-3), 1m, 2m, 3m, 4m, 5m);
        var quotes = new List<Quote> { withingSignalAge1, withingSignalAge2 };

        // Act
        var result = WaveTrendSignals.GetWtSignalsTradeAction(
            quotes,
            new WaveTrendSignal(1m, 2m, 3m, TradeAction.Buy),
            Minutes.GetMaxSignalAge(Granularity.FiveMins).Value
        );

        // Assert
        result.Should().Be(TradeAction.Buy);
    }

    [Fact]
    public void CreateWaveTrendSignals_IncorrectArgumentList_EmptyReturned()
    {
        // Arrange
        var waveTrendSettings = new WaveTrendSettings();

        // Act
        var result = WaveTrendSignals.CreateWaveTrendSignals(
            new List<WaveTrendResult>(),
            waveTrendSettings
        );

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void CreateWaveTrendSignals_IncorrectWt1AndWt2_ListWithNullsReturned()
    {
        // Arrange
        var waveTrendResultsWithZeroes = new List<WaveTrendResult>
        {
            new(0, 0, null),
            new(0, 0, null),
        };
        var waveTrendSettings = new WaveTrendSettings();

        // Act
        var result = WaveTrendSignals.CreateWaveTrendSignals(
            waveTrendResultsWithZeroes,
            waveTrendSettings
        );

        // Assert
        result.Should().HaveCount(2);
        result[1].TradeAction.Should().Be(TradeAction.Hold);
    }

    [Fact]
    public void CreateWaveTrendSignals_CrossesDownToUp_BuySignalReturned()
    {
        // Arrange
        var waveTrendResultsCrossDownToUp = new List<WaveTrendResult>
        {
            new(-2, -1, null),
            new(-3, -4, null),
        };
        var waveTrendSettings = new WaveTrendSettings { OversoldLevel2 = 1, Oversold = -40 };

        // Act
        var result = WaveTrendSignals.CreateWaveTrendSignals(
            waveTrendResultsCrossDownToUp,
            waveTrendSettings
        );

        // Assert
        result.Should().HaveCount(2);
        result[1].TradeAction.Should().Be(TradeAction.Buy);
    }

    [Fact]
    public void CreateWaveTrendSignals_CrossesUpToDown_SellSignalReturned()
    {
        // Arrange
        var waveTrendResultsCrossUpToDown = new List<WaveTrendResult>
        {
            new(2, 1, null),
            new(3, 4, null),
        };
        var waveTrendSettings = new WaveTrendSettings { OverboughtLevel2 = 1, Overbought = 20 };

        // Act
        var result = WaveTrendSignals.CreateWaveTrendSignals(
            waveTrendResultsCrossUpToDown,
            waveTrendSettings
        );

        // Assert
        result.Should().HaveCount(2);
        result[1].TradeAction.Should().Be(TradeAction.Sell);
    }

    [Fact]
    public void CreateWaveTrendSignals_NoCrosses_HoldSignalReturned()
    {
        // Arrange
        var waveTrendResultsNoCrosses = new List<WaveTrendResult>
        {
            new(1, 1, null),
            new(1, 1, null),
        };
        var waveTrendSettings = new WaveTrendSettings();

        // Act
        var result = WaveTrendSignals.CreateWaveTrendSignals(
            waveTrendResultsNoCrosses,
            waveTrendSettings
        );

        // Assert
        result.Should().HaveCount(2);
        result[0].TradeAction.Should().Be(TradeAction.Hold);
    }
}

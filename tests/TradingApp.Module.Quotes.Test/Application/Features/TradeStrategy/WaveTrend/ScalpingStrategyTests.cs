using FluentAssertions;
using NSubstitute;
using TradingApp.Domain.Modules.Constants;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.WaveTrend;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Enums;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.TradeStrategy.WaveTrend;

public class ScalpingStrategyTests
{
    private readonly IEvaluator _evaluator = Substitute.For<IEvaluator>();

    [Fact]
    public void EvaluateSignals_IncorrectGranularity_ReturnsFail()
    {
        // Arrange
        // Act
        var result = new ScalpingStrategy(_evaluator).EvaluateSignals(
            new List<Quote>(0),
            WaveTrendSettingsConst.WaveTrendSettingsDefault,
            Granularity.Hourly
        );

        // Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void EvaluateSignals_HoldExpected()
    {
        // Arrange
        _evaluator
            .GetWaveTrend(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<WaveTrendSettings>())
            .Returns(new List<WaveTrendResult> { new(1m, 2m, 3m), new(1m, 2m, 3m) });
        // Act
        var result = new ScalpingStrategy(_evaluator).EvaluateSignals(
            new List<Quote>(0),
            WaveTrendSettingsConst.WaveTrendSettingsDefault,
            Granularity.FiveMins
        );

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(2);
        result.Value[1].TradeAction.Should().Be(TradeAction.Hold);
    }

    [Fact]
    public void EvaluateSignals_IncorrectWt1AndWt2_ListWithNullsReturned()
    {
        // Arrange
        _evaluator
            .GetWaveTrend(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<WaveTrendSettings>())
            .Returns(new List<WaveTrendResult> { new(0, 0, null), new(0, 0, null) });
        var waveTrendSettings = new WaveTrendSettings();

        // Act
        var result = new ScalpingStrategy(_evaluator).EvaluateSignals(
            new List<Quote>(),
            waveTrendSettings,
            Granularity.FiveMins
        );

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(2);
        result.Value[1].TradeAction.Should().Be(TradeAction.Hold);
    }

    [Fact]
    public void EvaluateSignals_CrossesDownToUp_BuySignalReturned()
    {
        // Arrange
        _evaluator
            .GetWaveTrend(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<WaveTrendSettings>())
            .Returns(new List<WaveTrendResult> { new(-2, -1, null), new(-3, -4, null) });
        var waveTrendSettings = new WaveTrendSettings { OversoldLevel2 = 1, Oversold = -40 };

        // Act
        var result = new ScalpingStrategy(_evaluator).EvaluateSignals(
            new List<Quote>(),
            waveTrendSettings,
            Granularity.FiveMins
        );

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(2);
        result.Value[1].TradeAction.Should().Be(TradeAction.Buy);
    }

    [Fact]
    public void EvaluateSignals_CrossesUpToDown_SellSignalReturned()
    {
        // Arrange
        _evaluator
            .GetWaveTrend(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<WaveTrendSettings>())
            .Returns(new List<WaveTrendResult> { new(2, 1, null), new(3, 4, null) });
        var waveTrendSettings = new WaveTrendSettings { OverboughtLevel2 = 1, Overbought = 20 };

        // Act
        var result = new ScalpingStrategy(_evaluator).EvaluateSignals(
            new List<Quote>(),
            waveTrendSettings,
            Granularity.FiveMins
        );


        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(2);
        result.Value[1].TradeAction.Should().Be(TradeAction.Sell);
    }

    [Fact]
    public void EvaluateSignals_NoCrosses_HoldSignalReturned()
    {
        // Arrange
        _evaluator
            .GetWaveTrend(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<WaveTrendSettings>())
            .Returns(new List<WaveTrendResult> { new(1, 1, null), new(1, 1, null) });
        var waveTrendSettings = new WaveTrendSettings();

        // Act
        var result = new ScalpingStrategy(_evaluator).EvaluateSignals(
            new List<Quote>(),
            waveTrendSettings,
            Granularity.FiveMins
        );

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(2);
        result.Value[1].TradeAction.Should().Be(TradeAction.Hold);
    }
}


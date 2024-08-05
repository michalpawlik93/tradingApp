using FluentAssertions;
using NSubstitute;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Ports;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.TradeStrategy.Srsi;

public class SrsiStrategiesTests
{
    private readonly IEvaluator _evaluator = Substitute.For<IEvaluator>();

    [Fact]
    public void GetStrategy_Default_ReturnsEmaAndStochStrategy()
    {
        // Arrange
        // Act
        var result = new SrsiStrategyFactory(_evaluator).GetStrategy((TradingStrategy)100, (Granularity)100);

        // Assert
        result.Should().BeOfType<EmaAndStochStrategy>();
    }

    [Fact]
    public void GetStrategy_EmaAndStoch_ReturnsEmaAndStochStrategy()
    {
        // Arrange
        // Act
        var result = new SrsiStrategyFactory(_evaluator).GetStrategy(TradingStrategy.EmaAndStoch, Granularity.FiveMins);

        // Assert
        result.Should().BeOfType<EmaAndStochStrategy>();
    }

    [Fact]
    public void GetStrategy_Scalping_ReturnsScalpingStrategy()
    {
        // Arrange
        // Act
        var result = new SrsiStrategyFactory(_evaluator).GetStrategy(TradingStrategy.Scalping, Granularity.Hourly);

        // Assert
        result.Should().BeOfType<ScalpingStrategy>();
    }

    [Fact]
    public void GetStrategy_FiveMins_ReturnsScalpingStrategy()
    {
        // Arrange
        // Act
        var result = new SrsiStrategyFactory(_evaluator).GetStrategy((TradingStrategy)100, Granularity.FiveMins);

        // Assert
        result.Should().BeOfType<ScalpingStrategy>();
    }

    [Fact]
    public void GetStrategy_DailyTrading_ReturnsDailyTradingStrategy()
    {
        // Arrange
        // Act
        var result = new SrsiStrategyFactory(_evaluator).GetStrategy(TradingStrategy.DayTrading, Granularity.FiveMins);

        // Assert
        result.Should().BeOfType<DailyTradingStrategy>();
    }

    [Fact]
    public void GetStrategy_Hourly_ReturnsDailyTradingStrategy()
    {
        // Arrange
        // Act
        var result = new SrsiStrategyFactory(_evaluator).GetStrategy((TradingStrategy)100, Granularity.Hourly);

        // Assert
        result.Should().BeOfType<DailyTradingStrategy>();
    }
}


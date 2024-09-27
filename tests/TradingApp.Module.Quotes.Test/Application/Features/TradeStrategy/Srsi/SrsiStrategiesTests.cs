using FluentAssertions;
using NSubstitute;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Ports;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.TradeStrategy.Srsi;

public class SrsiStrategiesTests
{
    private readonly IEvaluator _evaluator = Substitute.For<IEvaluator>();

    [Fact]
    public void GetStrategy_Default_SrsiDefaultStrategy()
    {
        // Arrange
        // Act
        var result = new SrsiStrategyFactory(_evaluator).GetStrategy(
            (AssetName)100,
            (Granularity)100
        );

        // Assert
        result.Should().BeOfType<SrsiDefaultStrategy>();
    }

    [Fact]
    public void GetStrategy_Srsi5MinEurPlnStrategy()
    {
        // Arrange
        // Act
        var result = new SrsiStrategyFactory(_evaluator).GetStrategy(
            AssetName.EURPLN,
            Granularity.FiveMins
        );

        // Assert
        result.Should().BeOfType<Srsi5MinEurPlnStrategy>();
    }

    [Fact]
    public void GetStrategy_Srsi1hEurPlnStrategy()
    {
        // Arrange
        // Act
        var result = new SrsiStrategyFactory(_evaluator).GetStrategy(
            AssetName.EURPLN,
            Granularity.Hourly
        );

        // Assert
        result.Should().BeOfType<Srsi1hEurPlnStrategy>();
    }
}

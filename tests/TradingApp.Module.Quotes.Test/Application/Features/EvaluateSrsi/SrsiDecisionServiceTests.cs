using FluentAssertions;
using FluentResults;
using NSubstitute;
using TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Constants;
using TradingApp.Module.Quotes.Domain.Enums;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.EvaluateSrsi;

public class SrsiDecisionServiceTests
{
    private readonly ISrsiStrategyFactory _srsiStrategyFactory =
        Substitute.For<ISrsiStrategyFactory>();
    private readonly ISrsiStrategy _srsiStrategy = Substitute.For<ISrsiStrategy>();
    private readonly SrsiDecisionService _sut;

    public SrsiDecisionServiceTests()
    {
        _sut = new SrsiDecisionService(_srsiStrategyFactory);
    }

    [Fact]
    public void MakeDecision_EvaluateSignalsFails_ReturnFail()
    {
        //Arrange
        _srsiStrategyFactory
            .GetStrategy(Arg.Any<TradingStrategy>(), Arg.Any<Granularity>())
            .Returns(_srsiStrategy);
        _srsiStrategy.EvaluateSignals(Arg.Any<IReadOnlyList<Quote>>()).Returns(Result.Fail(""));

        var quotes = new List<Quote> { new(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m) };

        //Act
        var result = _sut.MakeDecision(
            quotes,
            new SrsiDecisionSettings(
                SRsiSettingsConst.SRsiSettingsDefault,
                1,
                Granularity.FiveMins,
                TradingStrategy.EmaAndStoch
            )
        );

        //Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void MakeDecision_LastElementEmpty_ReturnFail()
    {
        //Arrange
        _srsiStrategyFactory
            .GetStrategy(Arg.Any<TradingStrategy>(), Arg.Any<Granularity>())
            .Returns(_srsiStrategy);
        _srsiStrategy
            .EvaluateSignals(Arg.Any<IReadOnlyList<Quote>>())
            .Returns(Result.Ok((IReadOnlyList<SrsiSignal>)[]));

        var quotes = new List<Quote> { new(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m) };

        //Act
        var result = _sut.MakeDecision(
            quotes,
            new SrsiDecisionSettings(
                SRsiSettingsConst.SRsiSettingsDefault,
                1,
                Granularity.FiveMins,
                TradingStrategy.EmaAndStoch
            )
        );

        //Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void MakeDecision_ReturnCorrectDecision()
    {
        //Arrange
        var startValue1 = 0.4325M;
        var startValue2 = 0.4326M;

        var results = Enumerable
            .Range(0, 15)
            .Select(
                i =>
                    new SrsiSignal(
                        startValue1 + 0.001M * i,
                        startValue2 + 0.001M * i,
                        TradeAction.Hold
                    )
            )
            .ToList();

        var quotes = new List<Quote> { new(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m) };
        _srsiStrategyFactory
            .GetStrategy(Arg.Any<TradingStrategy>(), Arg.Any<Granularity>())
            .Returns(_srsiStrategy);
        _srsiStrategy
            .EvaluateSignals(Arg.Any<IReadOnlyList<Quote>>())
            .Returns(Result.Ok((IReadOnlyList<SrsiSignal>)results));

        //Act
        var result = _sut.MakeDecision(
            quotes,
            new SrsiDecisionSettings(
                SRsiSettingsConst.SRsiSettingsDefault,
                1,
                Granularity.FiveMins,
                TradingStrategy.EmaAndStoch
            )
        );

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Action.Should().Be(TradeAction.Hold);
    }
}

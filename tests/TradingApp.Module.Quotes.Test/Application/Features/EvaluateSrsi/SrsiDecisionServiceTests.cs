using FluentAssertions;
using NSubstitute;
using TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Constants;
using TradingApp.Module.Quotes.Domain.Enums;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.EvaluateSrsi;

public class SrsiDecisionServiceTests
{
    private readonly IEvaluator _evaluator = Substitute.For<IEvaluator>();
    private readonly SrsiDecisionService _sut;

    public SrsiDecisionServiceTests()
    {
        _sut = new SrsiDecisionService(_evaluator);
    }

    [Fact]
    public void MakeDecisions_ReturnFail()
    {
        //Arrange

        var quotes = new List<Quote> { new(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m) };

        //Act
        var result = _sut.GetQuotesTradeActions(
            quotes,
            new SrsiDecisionSettings(1, 2),
            SRsiSettingsConst.SRsiSettingsDefault
        );

        //Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void MakeDecision_IncorrectSrsiLength_ReturnFail()
    {
        //Arrange
        _evaluator
            .GetSrsi(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<SRsiSettings>())
            .Returns(new List<SRsiResult>());

        var quotes = new List<Quote> { new(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m) };

        //Act
        var result = _sut.MakeDecision(
            quotes,
            new SrsiDecisionSettings(1, 2),
            SRsiSettingsConst.SRsiSettingsDefault
        );

        //Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void MakeDecision_ReturnHold()
    {
        //Arrange
        var startDate = DateTime.Now;
        var startValue1 = 0.4325M;
        var startValue2 = 0.4326M;

        var results = Enumerable
            .Range(0, 15)
            .Select(
                i =>
                    new SRsiResult(
                        startDate.AddHours(i),
                        startValue1 + 0.001M * i,
                        startValue2 + 0.001M * i
                    )
            )
            .ToList();
        _evaluator
            .GetSrsi(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<SRsiSettings>())
            .Returns(results);

        var quotes = new List<Quote> { new(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m) };

        //Act
        var result = _sut.MakeDecision(
            quotes,
            new SrsiDecisionSettings(1, 2),
            SRsiSettingsConst.SRsiSettingsDefault
        );

        //Assert
        result.Value.Action.Should().Be(TradeAction.Hold);
    }
}

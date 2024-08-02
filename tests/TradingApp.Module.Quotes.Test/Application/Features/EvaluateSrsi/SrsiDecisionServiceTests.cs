using FluentAssertions;
using FluentResults;
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
    public void GetDecisionQuotes_EmeaFail_ReturnFail()
    {
        //Arrange
        _evaluator
            .GetEmea(Arg.Any<decimal[]>(), Arg.Any<int>())
            .Returns(Result.Fail(""));
        var quotes = new List<Quote> { new(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m) };
        //Act
        var result = _sut.GetDecisionQuotes(
            quotes,
            new SrsiDecisionSettings(SRsiSettingsConst.SRsiSettingsDefault, 1)
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
            new SrsiDecisionSettings(SRsiSettingsConst.SRsiSettingsDefault, 1)
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
            .GetEmea(Arg.Any<decimal[]>(), Arg.Any<int>())
            .Returns(Result.Ok(new decimal[] { 1m, 2m }));
        _evaluator
            .GetSrsi(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<SRsiSettings>())
            .Returns(results);

        var quotes = new List<Quote> { new(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m) };

        //Act
        var result = _sut.MakeDecision(
            quotes,
            new SrsiDecisionSettings(SRsiSettingsConst.SRsiSettingsDefault, 1)
        );

        //Assert
        result.Value.Action.Should().Be(TradeAction.Hold);
    }
}

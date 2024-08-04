using FluentAssertions;
using FluentResults;
using NSubstitute;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Enums;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.TradeStrategy.Srsi;

public class EmaAndStochStrategyTests
{
    private readonly IEvaluator _evaluator = Substitute.For<IEvaluator>();

    [Fact]
    public void EvaluateSignals_SrsiResultsEmpty_ReturnsFail()
    {
        // Arrange
        _evaluator
            .GetSrsi(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<SRsiSettings>())
            .Returns(new List<SRsiResult>(0));
        // Act
        var result = new EmaAndStochStrategy(_evaluator).EvaluateSignals(new List<Quote>());

        // Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void EvaluateSignals_GetEmeaFailed_ReturnsFail()
    {
        // Arrange
        var srsiResults = new List<SRsiResult>
        {
            new(DateTime.Now, 1m, 2m),
            new(DateTime.Now, 1m, 2m)
        };
        _evaluator
            .GetSrsi(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<SRsiSettings>())
            .Returns(srsiResults);
        _evaluator
            .GetEmea(Arg.Any<decimal[]>(), Arg.Any<int>())
            .Returns(
                Result.Fail(""),
                Result.Ok<decimal[]>([2m, 2m])
            );
        // Act
        var result = new EmaAndStochStrategy(_evaluator).EvaluateSignals(new List<Quote>());

        // Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void EvaluateSignals_GetEma2xFailed_ReturnsFail()
    {
        // Arrange
        var srsiResults = new List<SRsiResult>
        {
            new(DateTime.Now, 1m, 2m),
            new(DateTime.Now, 1m, 2m)
        };
        _evaluator
            .GetSrsi(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<SRsiSettings>())
            .Returns(srsiResults);
        _evaluator
            .GetEmea(Arg.Any<decimal[]>(), Arg.Any<int>())
            .Returns(
                Result.Ok<decimal[]>([2m, 2m]),
                Result.Fail("")
            );
        // Act
        var result = new EmaAndStochStrategy(_evaluator).EvaluateSignals(new List<Quote>());

        // Assert
        result.IsFailed.Should().BeTrue();
    }


    [Fact]
    public void EvaluateSignals_HoldExpected()
    {
        // Arrange
        var quote1 = new Quote(DateTime.Now, 1m, 2m, 3m, 1m, 5m);
        var quote2 = new Quote(DateTime.Now, 1m, 2m, 3m, 1m, 5m);
        var quotes = new List<Quote> { quote1, quote2 };
        var last = new SRsiResult(DateTime.Now, 1m, 2m);
        var penult = new SRsiResult(DateTime.Now, 1m, 2m);
        var srsiResults = new List<SRsiResult> { penult, last };
        _evaluator
            .GetSrsi(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<SRsiSettings>())
            .Returns(srsiResults);
        _evaluator
            .GetEmea(Arg.Any<decimal[]>(), Arg.Any<int>())
            .Returns(
                Result.Ok<decimal[]>([1m, 1m]),
                Result.Ok<decimal[]>([2m, 2m])
            );

        // Act
        var result = new EmaAndStochStrategy(_evaluator).EvaluateSignals(quotes);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value[0].TradeAction.Should().Be(TradeAction.Hold);
        result.Value[1].TradeAction.Should().Be(TradeAction.Hold);
    }

    [Fact]
    public void EvaluateSignals_SellExpected()
    {
        // Arrange
        var quote1 = new Quote(DateTime.Now, 1m, 2m, 3m, 40m, 5m);
        var quote2 = new Quote(DateTime.Now, 1m, 2m, 3m, 40m, 5m);
        var quotes = new List<Quote> { quote1, quote2 };
        var last = new SRsiResult(DateTime.Now, 55m, 70m);
        var penult = new SRsiResult(DateTime.Now, 95m, 75m);
        var srsiResults = new List<SRsiResult> { penult, last };
        _evaluator
            .GetSrsi(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<SRsiSettings>())
            .Returns(srsiResults);
        _evaluator
            .GetEmea(Arg.Any<decimal[]>(), Arg.Any<int>())
            .Returns(
                Result.Ok<decimal[]>(new decimal[] { 50m, 50m }),
                Result.Ok<decimal[]>(new decimal[] { 60m, 60m })
            );
        // Act
        var result = new EmaAndStochStrategy(_evaluator).EvaluateSignals(quotes);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value[0].TradeAction.Should().Be(TradeAction.Hold);
        result.Value[1].TradeAction.Should().Be(TradeAction.Sell);
    }

    [Fact]
    public void EvaluateSignals_BuyExpected()
    {
        // Arrange
        var latestClose = 70m;
        var quote1 = new Quote(DateTime.Now, 1m, 2m, 3m, latestClose, 5m);
        var quote2 = new Quote(DateTime.Now, 1m, 2m, 3m, latestClose, 5m);
        var quotes = new List<Quote> { quote1, quote2 };
        var last = new SRsiResult(DateTime.Now, 15m, 12m);
        var penult = new SRsiResult(DateTime.Now, 8m, 10m);
        var srsiResults = new List<SRsiResult> { penult, last };
        _evaluator
            .GetSrsi(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<SRsiSettings>())
            .Returns(srsiResults);
        _evaluator
            .GetEmea(Arg.Any<decimal[]>(), Arg.Any<int>())
            .Returns(
                Result.Ok<decimal[]>([60m, 60m]),
                Result.Ok<decimal[]>([50m, 50m])
            );
        // Act
        var result = new EmaAndStochStrategy(_evaluator).EvaluateSignals(quotes);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value[0].TradeAction.Should().Be(TradeAction.Hold);
        result.Value[1].TradeAction.Should().Be(TradeAction.Buy);
    }
}

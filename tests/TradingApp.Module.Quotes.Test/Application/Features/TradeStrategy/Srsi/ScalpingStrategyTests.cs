﻿using FluentAssertions;
using FluentResults;
using NSubstitute;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Enums;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.TradeStrategy.Srsi;

public class ScalpingStrategyTests
{
    private readonly IEvaluator _evaluator = Substitute.For<IEvaluator>();

    [Fact]
    public void EvaluateSignals_SrsiSettingsDisabled_ReturnsEmpty()
    {
        // Arrange
        // Act
        var result = new ScalpingStrategy(_evaluator).EvaluateSignals([], new SrsiSettings() { Enabled = false });

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }

    [Fact]
    public void EvaluateSignals_SrsiResultsEmpty_ReturnsFail()
    {
        // Arrange
        _evaluator
            .GetSrsi(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<SrsiSettings>())
            .Returns(new List<SRsiResult>(0));
        // Act
        var result = new ScalpingStrategy(_evaluator).EvaluateSignals([]);

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
            .GetSrsi(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<SrsiSettings>())
            .Returns(srsiResults);

        // Act
        var result = new ScalpingStrategy(_evaluator).EvaluateSignals(quotes);

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
            .GetSrsi(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<SrsiSettings>())
            .Returns(srsiResults);
        // Act
        var result = new ScalpingStrategy(_evaluator).EvaluateSignals(quotes);

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
        var last = new SRsiResult(DateTime.Now, 35m, 33m);
        var penult = new SRsiResult(DateTime.Now, 28m, 30m);
        var srsiResults = new List<SRsiResult> { penult, last };
        _evaluator
            .GetSrsi(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<SrsiSettings>())
            .Returns(srsiResults);
        _evaluator
            .GetEmea(Arg.Any<decimal[]>(), Arg.Any<int>())
            .Returns(
                Result.Ok<decimal[]>([60m, 60m]),
                Result.Ok<decimal[]>([50m, 50m])
            );
        // Act
        var result = new ScalpingStrategy(_evaluator).EvaluateSignals(quotes);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value[0].TradeAction.Should().Be(TradeAction.Hold);
        result.Value[1].TradeAction.Should().Be(TradeAction.Buy);
    }
}


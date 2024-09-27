using FluentAssertions;
using NSubstitute;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.TradeStrategy.Srsi;

public class Srsi1hEurPlnStrategyTests
{
    private readonly IEvaluator _evaluator = Substitute.For<IEvaluator>();

    [Fact]
    public void EvaluateSignals_SrsiResultsEmpty_ReturnsFail()
    {
        // Arrange
        _evaluator
            .GetSrsi(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<SrsiSettings>())
            .Returns(new List<SRsiResult>(0));
        // Act
        var result = new Srsi1hEurPlnStrategy(_evaluator).EvaluateSignals([]);

        // Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void EvaluateSignals_SrsiSettingsDisabled_ReturnsValidationError()
    {
        // Arrange
        // Act
        var result = new Srsi1hEurPlnStrategy(_evaluator).EvaluateSignals([], new SrsiSettings() { Enabled = false });

        // Assert
        result.IsFailed.Should().BeTrue();
        result.HasError<ValidationError>();
    }
}


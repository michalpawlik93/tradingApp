using FluentAssertions;
using TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;
using TradingApp.Module.Quotes.Application.Features.TradeSignals;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Constants;
using TradingApp.Module.Quotes.Domain.Enums;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.TradeSignals;

public class SrsiSignalsTests
{
    [Fact]
    public void GetTradeAction_HoldExpected()
    {
        // Arrange
        var last = new SRsiResult(DateTime.Now, 1m, 2m);
        var penult = new SRsiResult(DateTime.Now, 1m, 2m);
        var latestClose = 1m;

        // Act
        var result = SrsiSignals.GetTradeAction(
            latestClose,
            last,
            penult,
            new SrsiDecisionSettings(SRsiSettingsConst.SRsiSettingsDefault, 1m, 2m)
        );

        // Assert
        result.Should().Be(TradeAction.Hold);
    }

    [Fact]
    public void GetTradeAction_SellExpected()
    {
        // Arrange
        var last = new SRsiResult(DateTime.Now, 55m, 70m);
        var penult = new SRsiResult(DateTime.Now, 85m, 75m);
        var latestClose = 40m;
        var srsiSettings = new SRsiSettings(true, 14, 3, 3, -60, 60);
        var srsiDecisionSettings = new SrsiDecisionSettings(srsiSettings, 50m, 60m);

        // Act
        var result = SrsiSignals.GetTradeAction(
            latestClose,
            last,
            penult,
            srsiDecisionSettings
        );

        // Assert
        result.Should().Be(TradeAction.Sell);
    }

    [Fact]
    public void GetTradeAction_BuyExpected()
    {
        // Arrange
        var last = new SRsiResult(DateTime.Now, -40m, -55m);
        var penult = new SRsiResult(DateTime.Now, -85m, -75m);
        var latestClose = 70m;
        var srsiSettings = new SRsiSettings(true, 14, 3, 3, -60, 60);
        var srsiDecisionSettings = new SrsiDecisionSettings(srsiSettings, 60m, 50m);

        // Act
        var result = SrsiSignals.GetTradeAction(
            latestClose,
            last,
            penult,
            srsiDecisionSettings
        );

        // Assert
        result.Should().Be(TradeAction.Buy);
    }
}

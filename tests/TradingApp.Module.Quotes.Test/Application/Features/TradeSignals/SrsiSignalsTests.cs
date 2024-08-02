using FluentAssertions;
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
    public void CreateSriSignals_HoldExpected()
    {
        // Arrange
        var tooOldSignal = new Quote(DateTime.Now, 1m, 2m, 3m, 4m, 5m);
        var withingSignalAge = new Quote(DateTime.Now, 1m, 2m, 3m, 4m, 5m);
        var quotes = new List<Quote> { tooOldSignal, withingSignalAge };
        var srsiResults = new List<SRsiResult> { new(DateTime.Now, 1m, 2m), new(DateTime.Now, 1m, 2m) };
        // Act
        var result = SrsiSignals.CreateSriSignals(
            quotes,
            srsiResults,
            SRsiSettingsConst.SRsiSettingsDefault, [1m, 1m], [2m, 2m]
        );

        // Assert
        result.Should().HaveCount(2);
    }

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
            SRsiSettingsConst.SRsiSettingsDefault, 1m, 2m
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

        // Act
        var result = SrsiSignals.GetTradeAction(
            latestClose,
            last,
            penult,
            srsiSettings, 50m, 60m
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

        // Act
        var result = SrsiSignals.GetTradeAction(
            latestClose,
            last,
            penult,
            srsiSettings, 60m, 50m
        );

        // Assert
        result.Should().Be(TradeAction.Buy);
    }
}

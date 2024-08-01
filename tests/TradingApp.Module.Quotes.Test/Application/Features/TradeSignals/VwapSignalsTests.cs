using FluentAssertions;
using TradingApp.Module.Quotes.Application.Features.TradeSignals;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Domain.Enums;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.TradeSignals;


public class VwapSignalsTests
{
    public static IEnumerable<object[]> Data()
    {
        yield return new object[] { 1m, TradeAction.Buy };
        yield return new object[] { -1m, TradeAction.Sell };
    }

    [Theory, MemberData(nameof(Data))]
    public void GeVwapTradeAction(decimal vwap, TradeAction expected)
    {
        // Arrange
        // Act
        var result = VwapSignals.GeVwapTradeAction(
            new WaveTrendSignal(1m, 1m, vwap, TradeAction.Buy)
        );

        // Assert
        result.Should().Be(expected);
    }
}
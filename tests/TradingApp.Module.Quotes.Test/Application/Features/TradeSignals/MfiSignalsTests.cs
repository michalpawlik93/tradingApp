using FluentAssertions;
using TradingApp.Domain.Modules.Constants;
using TradingApp.Module.Quotes.Application.Features.TradeSignals;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Enums;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.TradeSignals;


public class MfiSignalsTests
{
    public static IEnumerable<object[]> MfiTradeActionData()
    {
        yield return new object[] { 90m, TradeAction.Sell };
        yield return new object[] { -90m, TradeAction.Buy };
        yield return new object[] { 30m, TradeAction.Hold };
    }

    [Theory, MemberData(nameof(MfiTradeActionData))]
    public void GeMfiTradeAction(decimal mfi, TradeAction expected)
    {
        // Arrange
        // Act
        var result = MfiSignals.GeMfiTradeAction(
            new MfiResult(mfi),
            WaveTrendSettingsConst.WaveTrendSettingsDefault
        );

        // Assert
        result.Should().Be(expected);
    }
}
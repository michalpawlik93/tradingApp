using FluentAssertions;
using TradingApp.Domain.Modules.Constants;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Enums;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.EvaluateCipherB;

public class CypherBDecisionServiceTests
{
    private readonly CypherBDecisionService _sut = new();
    private const decimal VwapBuy = 100;
    private const decimal VwapSell = -100;
    private const decimal MfiBuy = 100;
    private const decimal MfiSell = -100;

    [Fact]
    public void MakeDecision_QuotesIsEmpty_ErrorReturned()
    {
        //Arrange
        List<CypherBQuote> quotes = [];
        //Act
        var result = _sut.MakeDecision(new CypherBDecisionRequest(quotes, Granularity.FiveMins, WaveTrendSettingsConst.WaveTrendSettingsDefault
            ));

        //Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors[0].Message.Should().Be("Quotes is empty");
    }

    [Fact]
    public void MakeDecision_AllBuySignals_BuySignalReturned()
    {
        //Arrange
        var quotes = new List<CypherBQuote>()
        {
            new(
                new Quote(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m),
                new WaveTrendResult(1m, 2m, VwapBuy, true, null),
                new MfiResult(MfiSell)
            )
        };
        //Act
        var result = _sut.MakeDecision(new CypherBDecisionRequest(quotes, Granularity.FiveMins, WaveTrendSettingsConst.WaveTrendSettingsDefault));

        //Assert
        result.Errors.Should().BeEmpty();
        result.Value.IndexOutcome.Name.Should().Be("CipherB");
        result.Value.Action.Should().Be(TradeAction.Buy);
    }

    [Fact]
    public void MakeDecision_AllSellSignals_SellSignalReturned()
    {
        //Arrange
        var quotes = new List<CypherBQuote>()
        {
            new(
                new Quote(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m),
                new WaveTrendResult(1m, 2m, VwapSell, null, true),
                new MfiResult(MfiBuy)
            )
        };
        //Act
        var result = _sut.MakeDecision(new CypherBDecisionRequest(quotes, Granularity.FiveMins, WaveTrendSettingsConst.WaveTrendSettingsDefault));

        //Assert
        result.Errors.Should().BeEmpty();
        result.Value.IndexOutcome.Name.Should().Be("CipherB");
        result.Value.Action.Should().Be(TradeAction.Sell);
    }

    [Fact]
    public void MakeDecision_HoldSignalReturned()
    {
        //Arrange
        var quotes = new List<CypherBQuote>()
        {
            new(
                new Quote(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m),
                new WaveTrendResult(1m, 2m, VwapSell, true, null),
                new MfiResult(1m)
            )
        };
        //Act
        var result = _sut.MakeDecision(new CypherBDecisionRequest(quotes, Granularity.FiveMins, WaveTrendSettingsConst.WaveTrendSettingsDefault));

        //Assert
        result.Errors.Should().BeEmpty();
        result.Value.IndexOutcome.Name.Should().Be("CipherB");
        result.Value.Action.Should().Be(TradeAction.Hold);
    }
}


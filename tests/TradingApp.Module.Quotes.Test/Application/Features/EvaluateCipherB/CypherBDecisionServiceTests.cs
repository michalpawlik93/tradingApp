using FluentAssertions;
using NSubstitute;
using TradingApp.Domain.Modules.Constants;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Constants;
using TradingApp.Module.Quotes.Domain.Enums;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.EvaluateCipherB;

public class CypherBDecisionServiceTests
{
    private readonly IEvaluator _evaluator = Substitute.For<IEvaluator>();
    private readonly CypherBDecisionService _sut;
    private const decimal VwapBuy = 100;
    private const decimal VwapSell = -100;
    private const decimal MfiBuy = 100;
    private const decimal MfiSell = -100;

    public CypherBDecisionServiceTests()
    {
        _sut = new CypherBDecisionService(_evaluator);
    }
    [Fact]
    public void MakeDecision_QuotesIsEmpty_ErrorReturned()
    {
        //Arrange
        //Act
        var result = _sut.MakeDecision([], new CypherBDecisionSettings(Granularity.FiveMins, WaveTrendSettingsConst.WaveTrendSettingsDefault, MfiSettingsConst.MfiSettingsDefault)
            );

        //Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors[0].Message.Should().Be("Quotes is empty");
    }

    [Fact]
    public void MakeDecision_AllBuySignals_BuySignalReturned()
    {
        //Arrange
        _evaluator.GetMfi(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<MfiSettings>()).Returns(new List<MfiResult> { new(MfiSell), new(MfiSell) });
        _evaluator.GetWaveTrend(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<WaveTrendSettings>()).Returns(new List<WaveTrendResult> { new(-1m, -2m, VwapBuy), new(3m, 4m, VwapBuy) });

        var quotes = new List<Quote>
        {
            new(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m)
        };

        //Act
        var result = _sut.MakeDecision(quotes, new CypherBDecisionSettings(Granularity.FiveMins, WaveTrendSettingsConst.WaveTrendSettingsDefault, MfiSettingsConst.MfiSettingsDefault)
          );

        //Assert
        result.Errors.Should().BeEmpty();
        result.Value.IndexOutcome.Name.Should().Be("CipherB");
        result.Value.Action.Should().Be(TradeAction.Hold);
    }

    [Fact]
    public void GetQuotesTradeActions_MergedQuotesReturned()
    {
        //Arrange
        _evaluator.GetMfi(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<MfiSettings>()).Returns(new List<MfiResult> { new(MfiBuy), new(MfiBuy) });
        _evaluator.GetWaveTrend(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<WaveTrendSettings>()).Returns(new List<WaveTrendResult> { new(1m, 2m, VwapSell), new(1m, 2m, VwapSell) });
        var quotes = new List<Quote>
        {
            new(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m)
        };
        //Act
        var result = _sut.GetQuotesTradeActions(quotes, new CypherBDecisionSettings(Granularity.FiveMins, WaveTrendSettingsConst.WaveTrendSettingsDefault, MfiSettingsConst.MfiSettingsDefault));

        //Assert
        result.Errors.Should().BeEmpty();
        result.Value[0].WaveTrendSignalsResult.TradeAction.Should().Be(TradeAction.Hold);
    }
}


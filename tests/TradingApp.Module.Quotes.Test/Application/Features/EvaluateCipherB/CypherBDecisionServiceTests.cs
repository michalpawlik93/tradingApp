using FluentAssertions;
using FluentResults;
using NSubstitute;
using TradingApp.Domain.Modules.Constants;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.CipherB;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Constants;
using TradingApp.Module.Quotes.Domain.Enums;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.EvaluateCipherB;

public class CypherBDecisionServiceTests
{
    private readonly ICipherBStrategy _cipherBStrategy = Substitute.For<ICipherBStrategy>();
    private readonly CypherBDecisionService _sut;
    private const decimal VwapBuy = 100;
    private const decimal MfiSell = -100;

    public CypherBDecisionServiceTests()
    {
        _sut = new CypherBDecisionService(_cipherBStrategy);
    }

    [Fact]
    public void MakeDecision_SuccessPath_HoldSignalReturned()
    {
        //Arrange
        var mfiResults =
            (IReadOnlyList<MfiResult>)
                new List<MfiResult> { new(MfiSell), new(MfiSell) }.AsReadOnly();

        var waveTrendSignals =
            (IReadOnlyList<WaveTrendSignal>)
                new List<WaveTrendSignal>
                {
                    new(-1m, -2m, VwapBuy, TradeAction.Hold),
                    new(3m, 4m, VwapBuy, TradeAction.Hold)
                }.AsReadOnly();

        var srsiSignals =
            (IReadOnlyList<SrsiSignal>)
                new List<SrsiSignal>
                {
                    new(1m, 2m, TradeAction.Hold),
                    new(1m, 2m, TradeAction.Hold)
                }.AsReadOnly();

        _cipherBStrategy
            .EvaluateSignals(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<CypherBDecisionSettings>())
            .Returns(Result.Ok((mfiResults, waveTrendSignals, srsiSignals)));
        var quotes = new List<Quote> { new(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m) };

        //Act
        var result = _sut.MakeDecision(
            quotes,
            new CypherBDecisionSettings(
                Granularity.FiveMins,
                WaveTrendSettingsConst.WaveTrendSettingsDefault,
                MfiSettingsConst.MfiSettingsDefault,
                SRsiSettingsConst.SRsiSettingsDefault,
                AssetName.EURPLN
            )
        );

        //Assert
        result.Errors.Should().BeEmpty();
        result.Value.IndexOutcome.Name.Should().Be("CipherB");
        result.Value.Action.Should().Be(TradeAction.Hold);
    }
}

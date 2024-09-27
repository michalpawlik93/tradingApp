using FluentAssertions;
using NSubstitute;
using TradingApp.Domain.Modules.Constants;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.CipherB;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.WaveTrend;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Constants;
using TradingApp.Module.Quotes.Domain.Enums;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.TradeStrategy.CipherB;

public class CipherBStrategyTests
{
    private readonly IEvaluator _evaluator = Substitute.For<IEvaluator>();
    private readonly ISrsiStrategyFactory _srsiStrategyFactory =
        Substitute.For<ISrsiStrategyFactory>();
    private readonly ISrsiStrategy _srsiStrategy = Substitute.For<ISrsiStrategy>();
    private readonly IWaveTrendStrategy _waveTrendStrategy = Substitute.For<IWaveTrendStrategy>();
    private readonly IWaveTrendStrategyFactory _waveTrendStrategyFactory =
        Substitute.For<IWaveTrendStrategyFactory>();
    private readonly CipherBStrategy _sut;
    private const decimal VwapSell = -100;
    private const decimal MfiBuy = 100;

    public CipherBStrategyTests()
    {
        _sut = new CipherBStrategy(_evaluator, _srsiStrategyFactory, _waveTrendStrategyFactory);
    }

    [Fact]
    public void EvaluateSignals_MergedQuotesReturned()
    {
        //Arrange
        _evaluator
            .GetMfi(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<MfiSettings>())
            .Returns(new List<MfiResult> { new(MfiBuy), new(MfiBuy) });
        _srsiStrategyFactory
            .GetStrategy(Arg.Any<AssetName>(), Arg.Any<Granularity>())
            .Returns(_srsiStrategy);
        _srsiStrategy
            .EvaluateSignals(Arg.Any<IReadOnlyList<Quote>>())
            .Returns(
                new List<SrsiSignal>
                {
                    new(1m, 2m, TradeAction.Hold),
                    new(1m, 2m, TradeAction.Hold)
                }
            );
        _waveTrendStrategyFactory
            .GetStrategy(Arg.Any<AssetName>(), Arg.Any<Granularity>())
            .Returns(_waveTrendStrategy);
        _waveTrendStrategy
            .EvaluateSignals(
                Arg.Any<IReadOnlyList<Quote>>(),
                Arg.Any<WaveTrendSettings>(),
                Arg.Any<Granularity>()
            )
            .Returns(
                new List<WaveTrendSignal>
                {
                    new(1m, 2m, VwapSell, TradeAction.Hold),
                    new(1m, 2m, VwapSell, TradeAction.Hold)
                }
            );
        var quotes = new List<Quote> { new(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m) };
        //Act
        var result = _sut.EvaluateSignals(
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
        result.Value.waveTrendSignals[0].TradeAction.Should().Be(TradeAction.Hold);
    }
}

using AutoFixture.Xunit2;
using FluentAssertions;
using FluentResults;
using Moq;
using NSubstitute;
using System.Collections.Immutable;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Enums;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.GetCombinedQuotes;

public class GetCombinedQuotesCommandHandlerTests
{
    private readonly ITradingAdapter _adapter = Substitute.For<ITradingAdapter>();
    private readonly IEvaluator _evaluator = Substitute.For<IEvaluator>();
    private readonly ISrsiStrategyFactory _srsiStrategyFactory =
        Substitute.For<ISrsiStrategyFactory>();
    private readonly ISrsiStrategy _srsiStrategy = Substitute.For<ISrsiStrategy>();
    private readonly GetCombinedQuotesCommandHandler _sut;

    public GetCombinedQuotesCommandHandlerTests()
    {
        _sut = new GetCombinedQuotesCommandHandler(_adapter, _evaluator, _srsiStrategyFactory);
    }

    [Fact]
    public async Task Handle_GetQuotesFailed_ResponseReturned()
    {
        //Arrange
        var command = new GetCombinedQuotesCommand(
            new HashSet<TechnicalIndicator>().ToImmutableHashSet(),
            new TimeFrame(Granularity.Daily, null, null),
            new Asset(AssetName.ANC, AssetType.Currencies),
            TradingStrategy.Scalping
        );
        const string errorMessage = "errorMessage";
        _adapter
            .GetQuotes(
                command.TimeFrame,
                command.Asset,
                new PostProcessing(true),
                CancellationToken.None
            )
            .Returns(Result.Fail<IReadOnlyList<Quote>>(errorMessage));
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Errors.Should().Contain(x => x.Message == errorMessage);
    }

    [Theory]
    [AutoData]
    public async Task Handle_EvaluateSignalsFailed_ResponseReturned(List<Quote> quotes)
    {
        //Arrange
        var command = new GetCombinedQuotesCommand(
            new HashSet<TechnicalIndicator>() { TechnicalIndicator.Srsi }.ToImmutableHashSet(),
            new TimeFrame(Granularity.Daily, null, null),
            new Asset(AssetName.ANC, AssetType.Currencies),
            TradingStrategy.Scalping
        );
        _adapter
            .GetQuotes(
                command.TimeFrame,
                command.Asset,
                new PostProcessing(true),
                CancellationToken.None
            )
            .Returns(Result.Ok((IReadOnlyList<Quote>)quotes));
        _srsiStrategyFactory
            .GetStrategy(Arg.Any<TradingStrategy>(), Arg.Any<Granularity>())
            .Returns(_srsiStrategy);
        _srsiStrategy.EvaluateSignals(Arg.Any<IReadOnlyList<Quote>>()).Returns(Result.Fail(""));

        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_SuccessPath_ResponseReturned()
    {
        //Arrange
        var quotes = new Quote[] { new() };
        var command = new GetCombinedQuotesCommand(
            new HashSet<TechnicalIndicator>().ToImmutableHashSet(),
            new TimeFrame(Granularity.Daily, null, null),
            new Asset(AssetName.ANC, AssetType.Currencies),
            TradingStrategy.Scalping
        );
        _adapter
            .GetQuotes(
                command.TimeFrame,
                command.Asset,
                new PostProcessing(true),
                CancellationToken.None
            )
            .Returns(Result.Ok((IReadOnlyList<Quote>)quotes));
        var values = Enumerable
            .Range(0, quotes.Count())
            .Select(_ => new RsiResult() { Value = (decimal?)new Random().NextDouble() })
            .ToList();
        _evaluator
            .GetRsi(It.IsAny<IReadOnlyList<Quote>>(), It.IsAny<RsiSettings>())
            .Returns(values);

        var srsiSignals =
            (IReadOnlyList<SrsiSignal>)
                quotes.Select(_ => new SrsiSignal(1m, 1m, TradeAction.Buy)).ToList().AsReadOnly();

        _srsiStrategyFactory
            .GetStrategy(Arg.Any<TradingStrategy>(), Arg.Any<Granularity>())
            .Returns(_srsiStrategy);
        _srsiStrategy
            .EvaluateSignals(Arg.Any<IReadOnlyList<Quote>>())
            .Returns(Result.Ok(srsiSignals));
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Value.Quotes.Should().HaveCount(quotes.Count());
    }
}

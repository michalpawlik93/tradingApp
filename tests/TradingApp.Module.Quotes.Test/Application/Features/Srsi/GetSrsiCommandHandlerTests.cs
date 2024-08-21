using AutoFixture.Xunit2;
using FluentAssertions;
using FluentResults;
using NSubstitute;
using TradingApp.Module.Quotes.Application.Features.Srsi;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Enums;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.Srsi;

public class GetSrsiCommandHandlerTests
{
    private readonly ITradingAdapter _adapter = Substitute.For<ITradingAdapter>();
    private readonly ISrsiStrategyFactory _srsiStrategyFactory =
        Substitute.For<ISrsiStrategyFactory>();
    private readonly ISrsiStrategy _srsiStrategy = Substitute.For<ISrsiStrategy>();
    private readonly GetSrsiCommandHandler _sut;

    public GetSrsiCommandHandlerTests()
    {
        _sut = new GetSrsiCommandHandler(_adapter, _srsiStrategyFactory);
    }

    [Theory]
    [AutoData]
    public async Task Handle_GetQuotesFailed_ResponseReturned(GetSrsiCommand command)
    {
        //Arrange
        const string errorMessage = "errorMessage";
        _adapter
            .GetQuotes(
                command.TimeFrame,
                command.Asset,
                new PostProcessing(true),
                CancellationToken.None
            )
            .Returns(Result.Fail<IEnumerable<Quote>>(errorMessage));
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Errors.Should().Contain(x => x.Message == errorMessage);
    }

    [Theory]
    [AutoData]
    public async Task Handle_EvaluateSignalsFailed_ResponseReturned(
        GetSrsiCommand command,
        List<Quote> quotes
    )
    {
        // Arrange
        _adapter
            .GetQuotes(
                command.TimeFrame,
                command.Asset,
                new PostProcessing(true),
                CancellationToken.None
            )
            .Returns(Result.Ok((IEnumerable<Quote>)quotes));

        const string errorMessage = "errorMessage";
        _srsiStrategyFactory
            .GetStrategy(Arg.Any<TradingStrategy>(), Arg.Any<Granularity>())
            .Returns(_srsiStrategy);
        _srsiStrategy.EvaluateSignals(Arg.Any<IReadOnlyList<Quote>>()).Returns(Result.Fail(""));

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public async Task Handle_SuccessPath_ResponseReturned(
        GetSrsiCommand command,
        List<Quote> quotes
    )
    {
        // Arrange
        _adapter
            .GetQuotes(
                command.TimeFrame,
                command.Asset,
                new PostProcessing(true),
                CancellationToken.None
            )
            .Returns(Result.Ok((IEnumerable<Quote>)quotes));

        var srsiSignals =
            (IReadOnlyList<SrsiSignal>)
                quotes.Select(_ => new SrsiSignal(1m, 1m, TradeAction.Buy)).ToList().AsReadOnly();

        _srsiStrategyFactory
            .GetStrategy(Arg.Any<TradingStrategy>(), Arg.Any<Granularity>())
            .Returns(_srsiStrategy);
        _srsiStrategy
            .EvaluateSignals(Arg.Any<IReadOnlyList<Quote>>())
            .Returns(Result.Ok(srsiSignals));

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        result.Value.Srsi.Should().HaveCount(quotes.Count);
    }
}

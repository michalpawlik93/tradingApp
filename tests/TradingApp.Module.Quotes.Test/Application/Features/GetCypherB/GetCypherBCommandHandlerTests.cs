using AutoFixture.Xunit2;
using FluentAssertions;
using FluentResults;
using NSubstitute;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Application.Features.GetCypherB;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.CipherB;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Enums;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.GetCypherB;

public class GetCypherBCommandHandlerTests
{
    private readonly ITradingAdapter _adapter = Substitute.For<ITradingAdapter>();
    private readonly ICipherBStrategy _cipherBStrategy =
        Substitute.For<ICipherBStrategy>();
    private readonly GetCypherBCommandHandler _sut;

    public GetCypherBCommandHandlerTests()
    {
        _sut = new GetCypherBCommandHandler(_adapter, _cipherBStrategy);
    }

    [Theory]
    [AutoData]
    public async Task Handle_GetQuotesFailed_ResponseReturned(GetCypherBCommand command)
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
            .Returns(Result.Fail<IReadOnlyList<Quote>>(errorMessage));
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Errors.Should().Contain(x => x.Message == errorMessage);
    }

    [Theory]
    [AutoData]
    public async Task Handle_EvaluateSignalsFailed_ResponseReturned(
        GetCypherBCommand command,
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
            .Returns(Result.Ok((IReadOnlyList<Quote>)quotes));

        const string errorMessage = "errorMessage";
        _cipherBStrategy
            .EvaluateSignals(Arg.Any<IReadOnlyList<Quote>>(), Arg.Any<CypherBDecisionSettings>())
            .Returns(Result.Fail<(IReadOnlyList<MfiResult> mfiResults,
                IReadOnlyList<WaveTrendSignal> waveTrendSignals,
                IReadOnlyList<SrsiSignal> srsiSignals)>(errorMessage));

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        result.Errors.Should().Contain(x => x.Message == errorMessage);
    }

    [Theory]
    [AutoData]
    public async Task Handle_SuccessPath_ResponseReturned(
        GetCypherBCommand command,
        List<Quote> quotes,
        WaveTrendSignal waveTrend
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
            .Returns(Result.Ok((IReadOnlyList<Quote>)quotes));

        var mfiResults = (IReadOnlyList<MfiResult>)quotes.Select(_ => new MfiResult((decimal)new Random().NextDouble())).ToList().AsReadOnly();
        var waveTrendSignals = (IReadOnlyList<WaveTrendSignal>)quotes.Select(_ => waveTrend).ToList().AsReadOnly();
        var srsiSignals = (IReadOnlyList<SrsiSignal>)quotes.Select(_ => new SrsiSignal(1m, 1m, TradeAction.Buy)).ToList().AsReadOnly();

        _cipherBStrategy
            .EvaluateSignals(
                Arg.Any<IReadOnlyList<Quote>>(),
                Arg.Any<CypherBDecisionSettings>()
            )
            .Returns(Result.Ok((mfiResults, waveTrendSignals, srsiSignals)));

        // Act
        var result = await _sut.Handle(command, CancellationToken.None);

        // Assert
        result.Value.Quotes.Should().HaveCount(quotes.Count);
    }
}

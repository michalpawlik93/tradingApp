using AutoFixture.Xunit2;
using FluentAssertions;
using FluentResults;
using NSubstitute;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Application.Features.GetCypherB;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.GetCypherB;

public class GetCypherBCommandHandlerTests
{
    private readonly ITradingAdapter _adapter = Substitute.For<ITradingAdapter>();
    private readonly ICypherBDecisionService _cypherBDecisionService =
        Substitute.For<ICypherBDecisionService>();
    private readonly GetCypherBCommandHandler _sut;

    public GetCypherBCommandHandlerTests()
    {
        _sut = new GetCypherBCommandHandler(_adapter, _cypherBDecisionService);
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
            .Returns(Result.Fail<IEnumerable<Quote>>(errorMessage));
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Errors.Should().Contain(x => x.Message == errorMessage);
    }

    [Theory]
    [AutoData]
    public async Task Handle_SuccessPath_ResponseReturned(
        GetCypherBCommand command,
        List<Quote> quotes,
        WaveTrendSignalsResult waveTrend
    )
    {
        //Arrange
        _adapter
            .GetQuotes(
                command.TimeFrame,
                command.Asset,
                new PostProcessing(true),
                CancellationToken.None
            )
            .Returns(Result.Ok((IEnumerable<Quote>)quotes));

        var cypherBResults = Enumerable
            .Range(0, quotes.Count)
            .Select(
                x =>
                    new CypherBQuote(
                        quotes[x],
                        waveTrend,
                        new MfiResult((decimal)new Random().NextDouble())
                    )
            )
            .ToList();

        _cypherBDecisionService
            .GetQuotesTradeActions(
                Arg.Any<IReadOnlyList<Quote>>(),
                Arg.Any<CypherBDecisionSettings>()
            )
            .Returns(Result.Ok((IReadOnlyList<CypherBQuote>)cypherBResults));
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Value.Quotes.Should().HaveCount(quotes.Count);
    }
}

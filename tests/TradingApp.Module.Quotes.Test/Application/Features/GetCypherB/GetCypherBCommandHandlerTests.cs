using AutoFixture.Xunit2;
using FluentAssertions;
using FluentResults;
using Moq;
using TradingApp.Module.Quotes.Application.Features.GetCypherB;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.GetCypherB;

public class GetCypherBCommandHandlerTests
{
    private readonly Mock<ITradingAdapter> _adapter = new();
    private readonly Mock<IEvaluator> _evaluator = new();
    private readonly GetCypherBCommandHandler _sut;

    public GetCypherBCommandHandlerTests()
    {
        _sut = new GetCypherBCommandHandler(_adapter.Object, _evaluator.Object);
    }

    [Theory]
    [AutoData]
    public async Task Handle_GetQuotesFailed_ResponseReturned(GetCypherBCommand command)
    {
        //Arrange
        const string errorMessage = "errorMessage";
        _adapter
            .Setup(
                _ =>
                    _.GetQuotes(
                        command.TimeFrame,
                        command.Asset,
                        new PostProcessing(true),
                        CancellationToken.None
                    )
            )
            .ReturnsAsync(Result.Fail<IEnumerable<Quote>>(errorMessage));
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Errors.Should().Contain(x => x.Message == errorMessage);
    }

    [Theory]
    [AutoData]
    public async Task Handle_SuccessPath_ResponseReturned(
        GetCypherBCommand command,
        IEnumerable<Quote> quotes,
        WaveTrendResult waveTrend
    )
    {
        //Arrange
        _adapter
            .Setup(
                _ =>
                    _.GetQuotes(
                        command.TimeFrame,
                        command.Asset,
                        new PostProcessing(true),
                        CancellationToken.None
                    )
            )
            .ReturnsAsync(Result.Ok(quotes));
        var values = Enumerable
            .Range(0, quotes.Count())
            .Select(_ => new MfiResult((decimal)new Random().NextDouble()))
            .ToList();
        var waveTrends = Enumerable.Range(0, quotes.Count()).Select(_ => waveTrend).ToList();
        _evaluator.Setup(_ => _.GetMfi(It.IsAny<IEnumerable<Quote>>(), It.IsAny<MfiSettings>())).Returns(values);
        _evaluator
            .Setup(_ => _.GetWaveTrend(It.IsAny<IEnumerable<Quote>>(), It.IsAny<WaveTrendSettings>()))
            .Returns(waveTrends);
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Value.Quotes.Should().HaveCount(quotes.Count());
    }
}

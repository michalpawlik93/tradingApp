using AutoFixture.NUnit3;
using FluentAssertions;
using FluentResults;
using Moq;
using TradingApp.Module.Quotes.Application.GetCypherB;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Ports;

namespace TradingApp.Module.Quotes.Test.Quotes.Application.GetCypherB;

public class GetCypherBCommandHandlerTests
{
    private readonly Mock<ITradingAdapter> StooqProvider = new();
    private readonly Mock<IEvaluator> Evaluator = new();
    private GetCypherBCommandHandler _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new GetCypherBCommandHandler(StooqProvider.Object, Evaluator.Object);
    }

    [Test]
    [AutoData]
    public async Task Handle_GetQuotesFailed_ResponseReturned(GetCypherBCommand command)
    {
        //Arrange
        const string errrorMessage = "errorMessage";
        StooqProvider.Setup(_ => _.GetQuotes(new GetQuotesRequest(command.TimeFrame, command.Asset, new PostProcessing(true)))).ReturnsAsync(Result.Fail<IEnumerable<Quote>>(errrorMessage));
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Messages.Should().Contain(x => x.Message == errrorMessage);
    }

    [Test]
    [AutoData]
    public async Task Handle_SuccessPath_ResponseReturned(GetCypherBCommand command, IEnumerable<Quote> quotes, WaveTrendResult waveTrend)
    {
        //Arrange
        StooqProvider.Setup(_ => _.GetQuotes(new GetQuotesRequest(command.TimeFrame, command.Asset, new PostProcessing(true)))).ReturnsAsync(Result.Ok(quotes));
        var values = Enumerable.Range(0, quotes.Count()).Select(_ => new VWapResult() { Value = (decimal?)new Random().NextDouble() }).ToList();
        var waveTrends = Enumerable.Range(0, quotes.Count()).Select(_ => waveTrend).ToList();
        Evaluator.Setup(_ => _.GetVwap(It.IsAny<List<Quote>>())).Returns(values);
        Evaluator.Setup(_ => _.GetWaveTrend(It.IsAny<List<Quote>>(), It.IsAny<WaveTrendSettings>())).Returns(waveTrends);
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Data.Quotes.Should().HaveCount(quotes.Count());
    }
}

using AutoFixture.NUnit3;
using FluentAssertions;
using FluentResults;
using Moq;
using TradingApp.Application.Quotes.GetCypherB;
using TradingApp.StooqProvider;
using TradingApp.TradingAdapter.Evaluator;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Application.Test.Quotes;

public class GetCypherBCommandHandlerTests
{
    private readonly Mock<IStooqProvider> StooqProvider = new();
    private readonly Mock<ISkenderEvaluator> Evaluator = new();
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
        StooqProvider.Setup(_ => _.GetQuotes(new GetQuotesRequest(command.TimeFrame, command.Asset, null))).ReturnsAsync(Result.Fail<IEnumerable<Quote>>(errrorMessage));
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Messages.Should().Contain(x => x.Message == errrorMessage);
    }

    [Test]
    [AutoData]
    public async Task Handle_SuccessPath_ResponseReturned(GetCypherBCommand command, IEnumerable<Quote> quotes)
    {
        //Arrange
        StooqProvider.Setup(_ => _.GetQuotes(new GetQuotesRequest(command.TimeFrame, command.Asset, null))).ReturnsAsync(Result.Ok(quotes));
        var values = Enumerable.Range(0, quotes.Count()).Select(_ => (double?)new Random().NextDouble()).ToList();
        Evaluator.Setup(_ => _.GetVwap(It.IsAny<IEnumerable<Quote>>())).Returns(values);
        Evaluator.Setup(_ => _.GetMFI(It.IsAny<IEnumerable<Quote>>(), It.IsAny<int>())).Returns(values);
        Evaluator.Setup(_ => _.GetMomentumWave(It.IsAny<IEnumerable<Quote>>(), It.IsAny<int>())).Returns(values);
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Data.Quotes.Should().HaveCount(quotes.Count());
    }
}

using AutoFixture.NUnit3;
using FluentAssertions;
using FluentResults;
using Moq;
using TradingApp.Modules.Quotes.GetStooqQuotes;
using TradingApp.StooqProvider;
using TradingApp.TradingAdapter.Interfaces;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Modules.Test.Quotes;

public class GetStooqCombinedQuotesCommandHandlerTests
{
    private readonly Mock<IStooqProvider> StooqProvider = new();
    private readonly Mock<IEvaluator> Evaluator = new();
    private GetStooqCombinedQuotesCommandHandler _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new GetStooqCombinedQuotesCommandHandler(StooqProvider.Object, Evaluator.Object);
    }

    [Test]
    [AutoData]
    public async Task Handle_GetQuotesFailed_ResponseReturned(GetStooqCombinedQuotesCommand command)
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
    public async Task Handle_SuccessPath_ResponseReturned(GetStooqCombinedQuotesCommand command, IEnumerable<Quote> quotes)
    {
        //Arrange
        StooqProvider.Setup(_ => _.GetQuotes(new GetQuotesRequest(command.TimeFrame, command.Asset, new PostProcessing(true)))).ReturnsAsync(Result.Ok(quotes));
        var values = Enumerable.Range(0, quotes.Count()).Select(_ => new RsiResult() { Value = (decimal?)new Random().NextDouble() }).ToList();
        Evaluator.Setup(_ => _.GetRSI(It.IsAny<List<Quote>>(), It.IsAny<RsiSettings>())).Returns(values);
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Data.Quotes.Should().HaveCount(quotes.Count());
    }
}

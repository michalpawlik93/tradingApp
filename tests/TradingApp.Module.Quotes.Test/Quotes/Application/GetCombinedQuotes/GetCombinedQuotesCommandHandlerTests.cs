using FluentAssertions;
using FluentResults;
using Moq;
using System.Collections.Immutable;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Quotes.Application.GetCombinedQuotes;

public class GetCombinedQuotesCommandHandlerTests
{
    private readonly Mock<ITradingAdapter> _adapter = new();
    private readonly Mock<IEvaluator> _evaluator = new();
    private readonly GetCombinedQuotesCommandHandler _sut;

    public GetCombinedQuotesCommandHandlerTests()
    {
        _sut = new GetCombinedQuotesCommandHandler(_adapter.Object, _evaluator.Object);
    }

    [Fact]
    public async Task Handle_GetQuotesFailed_ResponseReturned()
    {
        //Arrange
        var command = new GetCombinedQuotesCommand(
            new HashSet<TechnicalIndicator>().ToImmutableHashSet(),
            new TimeFrame(Granularity.Daily, null, null),
            new Asset(AssetName.ANC, AssetType.Currencies)
        );
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

    [Fact]
    public async Task Handle_SuccessPath_ResponseReturned()
    {
        //Arrange
        var quotes = new Quote[] { new() };
        var command = new GetCombinedQuotesCommand(
            new HashSet<TechnicalIndicator>().ToImmutableHashSet(),
            new TimeFrame(Granularity.Daily, null, null),
            new Asset(AssetName.ANC, AssetType.Currencies)
        );
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
            .ReturnsAsync(quotes);
        var values = Enumerable
            .Range(0, quotes.Count())
            .Select(_ => new RsiResult() { Value = (decimal?)new Random().NextDouble() })
            .ToList();
        _evaluator
            .Setup(_ => _.GetRsi(It.IsAny<IEnumerable<Quote>>(), It.IsAny<RsiSettings>()))
            .Returns(values);
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Value.Quotes.Should().HaveCount(quotes.Count());
    }
}

﻿using AutoFixture.Xunit2;
using FluentAssertions;
using FluentResults;
using Moq;
using TradingApp.Module.Quotes.Application.Features.GetStooqCombinedQuotes;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Quotes.Application.GetStooqCombinedQuotes;

public class GetStooqCombinedQuotesCommandHandlerTests
{
    private readonly Mock<ITradingAdapter> _adapter = new();
    private readonly Mock<IEvaluator> Evaluator = new();
    private readonly GetStooqCombinedQuotesCommandHandler _sut;

    public GetStooqCombinedQuotesCommandHandlerTests()
    {
        _sut = new GetStooqCombinedQuotesCommandHandler(_adapter.Object, Evaluator.Object);
    }


    [Theory]
    [AutoData]
    public async Task Handle_GetQuotesFailed_ResponseReturned(GetStooqCombinedQuotesCommand command)
    {
        //Arrange
        const string errorMessage = "errorMessage";
        _adapter.Setup(_ => _.GetQuotes(command.TimeFrame, command.Asset, new PostProcessing(true), CancellationToken.None)).ReturnsAsync(Result.Fail<IEnumerable<Quote>>(errorMessage));
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Messages.Should().Contain(x => x.Message == errorMessage);
    }


    [Theory]
    [AutoData]
    public async Task Handle_SuccessPath_ResponseReturned(GetStooqCombinedQuotesCommand command, IEnumerable<Quote> quotes)
    {
        //Arrange
        _adapter.Setup(_ => _.GetQuotes(command.TimeFrame, command.Asset, new PostProcessing(true), CancellationToken.None)).ReturnsAsync(Result.Ok(quotes));
        var values = Enumerable.Range(0, quotes.Count()).Select(_ => new RsiResult() { Value = (decimal?)new Random().NextDouble() }).ToList();
        Evaluator.Setup(_ => _.GetRSI(It.IsAny<List<Quote>>(), It.IsAny<RsiSettings>())).Returns(values);
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Data.Quotes.Should().HaveCount(quotes.Count());
    }
}

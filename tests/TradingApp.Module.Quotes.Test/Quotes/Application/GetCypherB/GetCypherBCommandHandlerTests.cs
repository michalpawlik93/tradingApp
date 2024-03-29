﻿using AutoFixture.Xunit2;
using FluentAssertions;
using FluentResults;
using Moq;
using TradingApp.Module.Quotes.Application.Features.GetCypherB;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Application.Services;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Quotes.Application.GetCypherB;

public class GetCypherBCommandHandlerTests
{
    private readonly Mock<ITradingAdapter> StooqProvider = new();
    private readonly Mock<IEvaluator> Evaluator = new();
    private readonly GetCypherBCommandHandler _sut;

    public GetCypherBCommandHandlerTests()
    {
        _sut = new GetCypherBCommandHandler(StooqProvider.Object, Evaluator.Object);
    }

    [Theory]
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

    [Theory]
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

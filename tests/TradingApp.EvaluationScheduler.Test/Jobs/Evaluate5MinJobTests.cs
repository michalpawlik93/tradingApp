using FluentResults;
using MediatR;
using NSubstitute;
using Quartz;
using TradingApp.EvaluationScheduler.Jobs;
using TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Application.Services;
using TradingApp.Module.Quotes.Contract.Models;
using Xunit;

namespace TradingApp.EvaluationScheduler.Test.Integration;

public class Evaluate5MinJobTests
{
    private readonly ITradingAdapter _tradingAdapter = Substitute.For<ITradingAdapter>();
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly IJobExecutionContext _jobExecutionContext = Substitute.For<IJobExecutionContext>();
    private readonly Evaluate5MinJob _sut;

    public Evaluate5MinJobTests()
    {
        _sut = new Evaluate5MinJob(_tradingAdapter, _mediator);
    }

    [Fact]
    public async Task Execute_CommandSent()
    {
        //Arrange
        var expectedQuotes = new List<Quote>() { new Quote(), new Quote() };
        _tradingAdapter.GetQuotes(Arg.Any<GetQuotesRequest>()).Returns(Result.Ok<IEnumerable<Quote>>(expectedQuotes));
        //Act
        await _sut.Execute(_jobExecutionContext);
        //Assert
        await _tradingAdapter.Received().GetQuotes(Arg.Any<GetQuotesRequest>());
        await _mediator.Received().Send(Arg.Any<EvaluateSRsiCommand>());
    }
}

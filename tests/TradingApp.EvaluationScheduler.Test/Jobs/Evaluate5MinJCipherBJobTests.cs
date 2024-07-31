using FluentResults;
using MediatR;
using NSubstitute;
using Quartz;
using TradingApp.EvaluationScheduler.Jobs;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using Xunit;

namespace TradingApp.EvaluationScheduler.Test.Jobs;

public class Evaluate5MinJCipherBJobTests
{
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly ITradingAdapter _tradingAdapter = Substitute.For<ITradingAdapter>();
    private readonly IJobExecutionContext _jobExecutionContext =
        Substitute.For<IJobExecutionContext>();
    private readonly Evaluate5MinJCipherBJob _sut;

    public Evaluate5MinJCipherBJobTests()
    {
        _sut = new Evaluate5MinJCipherBJob(_mediator, _tradingAdapter);
    }

    [Fact]
    public async Task Execute_CommandSent()
    {
        //Arrange
        _mediator.Send(Arg.Any<EvaluateCipherBCommand>()).Returns(Result.Ok());
        _tradingAdapter
            .GetQuotes(
                Arg.Any<TimeFrame>(),
                Arg.Any<Asset>(),
                Arg.Any<PostProcessing>(),
                Arg.Any<CancellationToken>()
            )
            .Returns(Result.Ok((IEnumerable<Quote>)[new(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m)]));
        //Act
        await _sut.Execute(_jobExecutionContext);
        //Assert
        await _mediator.Received().Send(Arg.Any<EvaluateCipherBCommand>());
    }
}

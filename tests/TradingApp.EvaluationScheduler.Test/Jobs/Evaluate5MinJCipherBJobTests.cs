using FluentResults;
using MediatR;
using NSubstitute;
using Quartz;
using TradingApp.EvaluationScheduler.Jobs;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Application.Features.GetCypherB;
using TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using Xunit;

namespace TradingApp.EvaluationScheduler.Test.Jobs;

public class Evaluate5MinJCipherBJobTests
{
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly IJobExecutionContext _jobExecutionContext =
        Substitute.For<IJobExecutionContext>();
    private readonly Evaluate5MinJCipherBJob _sut;

    public Evaluate5MinJCipherBJobTests()
    {
        _sut = new Evaluate5MinJCipherBJob(_mediator);
    }

    [Fact]
    public async Task Execute_CommandSent()
    {
        //Arrange

        var quotes = new List<CypherBQuote>()
        {
            new(
                new Quote(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m),
                new WaveTrendResult(1m, 2m, 3m, true, null),
                new MfiResult(1m)
            )
        };
        _mediator
            .Send(Arg.Any<GetCypherBCommand>())
            .Returns(Result.Ok(new GetCypherBResponseDto(quotes)));
        //Act
        await _sut.Execute(_jobExecutionContext);
        //Assert
        await _mediator.Received().Send(Arg.Any<GetCypherBCommand>());
        await _mediator.Received().Send(Arg.Any<EvaluateCipherBCommand>());
    }
}

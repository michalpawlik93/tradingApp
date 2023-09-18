using AutoFixture.Xunit2;
using FluentAssertions;
using NSubstitute;
using TradingApp.Core.Domain;
using TradingApp.Core.EventBus;
using TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Application.Services;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.ValueObjects;
using TradingApp.Module.Quotes.Ports;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Quotes.Application.EvaluateSrsi;

public class EvaluateSRsiCommandHandlerTests
{
    private readonly IEventBus EventBus = Substitute.For<IEventBus>();
    private readonly IEvaluator Evaluator = Substitute.For<IEvaluator>();
    private readonly IDecisionService DecisionService = Substitute.For<IDecisionService>();
    private readonly IEntityDataService<Decision> DecisionDataService = Substitute.For<IEntityDataService<Decision>>();
    private readonly EvaluateSRsiCommandHandler _sut;

    public EvaluateSRsiCommandHandlerTests()
    {
        _sut = new EvaluateSRsiCommandHandler(EventBus, Evaluator, DecisionService, DecisionDataService);
    }

    [Theory]
    [AutoData]
    public async Task Handle_GetRSIReturnsEmptyList_EarlyReturn(EvaluateSRsiCommand command)
    {
        //Arrange
        Evaluator.GetSRSI(Arg.Any<List<Quote>>(), Arg.Any<SRsiSettings>()).Returns(new List<SRsiResult>());
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Messages.Should().NotBeEmpty();
        DecisionService.Received(0).MakeDecision(Arg.Any<IndexOutcome>());
        await EventBus.Received(0).Publish(Arg.Any<IAggregateRoot>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [AutoData]
    public async Task Handle_GetRSIReturnsList_DecisionSavedInDb_AggregateSentToEB(EvaluateSRsiCommand command, SRsiResult rsiResult)
    {
        //Arrange
        var secondResult = rsiResult with { StochD = 0, StochK = 2 };
        var rsiResults = new List<SRsiResult>() { rsiResult, secondResult };
        Evaluator.GetSRSI(Arg.Any<List<Quote>>(), Arg.Any<SRsiSettings>()).Returns(rsiResults);
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Messages.Should().BeEmpty();
        DecisionService.Received().MakeDecision(Arg.Any<IndexOutcome>());
        await EventBus.Received().Publish(Arg.Any<IAggregateRoot>(), Arg.Any<CancellationToken>());
        await DecisionDataService.Received().Add(Arg.Any<Decision>(), Arg.Any<CancellationToken>());
    }
}

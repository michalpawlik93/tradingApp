﻿using AutoFixture.Xunit2;
using FluentAssertions;
using NSubstitute;
using TradingApp.Core.Domain;
using TradingApp.Core.EventBus;
using TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Application.Services;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.ValueObjects;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Quotes.Application.EvaluateSrsi;

public class EvaluateSRsiCommandHandlerTests
{
    private readonly IEventBus _eventBus = Substitute.For<IEventBus>();
    private readonly IEvaluator _evaluator = Substitute.For<IEvaluator>();
    private readonly IDecisionService _decisionService = Substitute.For<IDecisionService>();
    private readonly IEntityDataService<Decision> _decisionDataService = Substitute.For<
        IEntityDataService<Decision>
    >();
    private readonly EvaluateSRsiCommandHandler _sut;

    public EvaluateSRsiCommandHandlerTests()
    {
        _sut = new EvaluateSRsiCommandHandler(
            _eventBus,
            _evaluator,
            _decisionService,
            _decisionDataService
        );
    }

    [Theory]
    [AutoData]
    public async Task Handle_GetRSIReturnsEmptyList_EarlyReturn(EvaluateSRsiCommand command)
    {
        //Arrange
        _evaluator
            .GetSRSI(Arg.Any<List<Quote>>(), Arg.Any<SRsiSettings>())
            .Returns(new List<SRsiResult>());

        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Errors.Should().NotBeEmpty();
        _decisionService.Received(0).MakeDecision(Arg.Any<IndexOutcome>());
        await _eventBus
            .Received(0)
            .Publish(Arg.Any<IAggregateRoot>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [AutoData]
    public async Task Handle_GetRSIReturnsList_DecisionSavedInDb_AggregateSentToEB(
        EvaluateSRsiCommand command,
        SRsiResult rsiResult
    )
    {
        //Arrange
        var secondResult = rsiResult with
        {
            StochD = 0,
            StochK = 2
        };

        var rsiResults = new List<SRsiResult>() { rsiResult, secondResult };
        _evaluator.GetSRSI(Arg.Any<List<Quote>>(), Arg.Any<SRsiSettings>()).Returns(rsiResults);

        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Errors.Should().BeEmpty();
        _decisionService.Received().MakeDecision(Arg.Any<IndexOutcome>());
        await _eventBus.Received().Publish(Arg.Any<IAggregateRoot>(), Arg.Any<CancellationToken>());
        await _decisionDataService
            .Received()
            .Add(Arg.Any<Decision>(), Arg.Any<CancellationToken>());
    }
}

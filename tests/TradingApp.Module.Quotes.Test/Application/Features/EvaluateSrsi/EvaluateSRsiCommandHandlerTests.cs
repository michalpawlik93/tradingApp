using FluentAssertions;
using NSubstitute;
using TradingApp.Core.Domain;
using TradingApp.Core.EventBus;
using TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Constants;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.EvaluateSrsi;

public class EvaluateSRsiCommandHandlerTests
{
    private readonly IEventBus _eventBus = Substitute.For<IEventBus>();
    private readonly ISrsiDecisionService _decisionService = Substitute.For<ISrsiDecisionService>();
    private readonly IEntityDataService<Decision> _decisionDataService = Substitute.For<
        IEntityDataService<Decision>
    >();
    private readonly EvaluateSRsiCommandHandler _sut;

    public EvaluateSRsiCommandHandlerTests()
    {
        _sut = new EvaluateSRsiCommandHandler(_eventBus, _decisionService, _decisionDataService);
    }

    [Fact]
    public async Task Handle_GetRSIReturnsList_DecisionSavedInDb_AggregateSentToEB()
    {
        //Arrange
        _decisionService
            .MakeDecision(
                Arg.Any<IReadOnlyList<Quote>>(),
                Arg.Any<SrsiDecisionSettings>()
            )
            .Returns(
                Decision.CreateNew(
                    new IndexOutcome("SRSI", 2.02356M),
                    DateTime.Now,
                    TradeAction.Buy,
                    MarketDirection.Bullish
                )
            );
        var quotes = new List<Quote> { new(DateTime.UtcNow, 1m, 2m, 3m, 4m, 5m) };

        var command = new EvaluateSRsiCommand(
            quotes,
            new SrsiDecisionSettings(SRsiSettingsConst.SRsiSettingsDefault, 1, Granularity.Hourly, TradingStrategy.EmaAndStoch)
        );
        //Act
        var result = await _sut.Handle(command, CancellationToken.None);

        //Assert
        result.Errors.Should().BeEmpty();
        _decisionService
            .Received()
            .MakeDecision(
                Arg.Any<IReadOnlyList<Quote>>(),
                Arg.Any<SrsiDecisionSettings>()
            );
        await _eventBus.Received().Publish(Arg.Any<IAggregateRoot>(), Arg.Any<CancellationToken>());
        await _decisionDataService
            .Received()
            .Add(Arg.Any<Decision>(), Arg.Any<CancellationToken>());
    }
}

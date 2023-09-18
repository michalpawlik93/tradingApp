using FluentAssertions;
using NSubstitute;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;
using TradingApp.MongoDb.Adapters;
using TradingApp.MongoDb.Mappers;
using TradingApp.MongoDb.Models;
using TradingApp.MongoDb.Test.Fixtures;

namespace TradingApp.MongoDb.Test.Integration;

[Collection(nameof(MongoDbFixtureCollection))]
public class MongoDataServiceTests
{
    private readonly IMongoDbMapper<Decision, DecisionDao> Mapper = Substitute.For<
        IMongoDbMapper<Decision, DecisionDao>
    >();
    private readonly MongoDataService<Decision, DecisionDao> _sut;

    public MongoDataServiceTests(MongoDbFixture fixture)
    {
        _sut = new MongoDataService<Decision, DecisionDao>(fixture.MongoDatabase, Mapper);
    }

    [Fact]
    public async Task AddAndGet_Success()
    {
        //Arrange
        var decision = Decision.CreateNew(
                new IndexOutcome("RSI", 0.023M),
                DateTime.UtcNow,
                TradeAction.Buy,
                new SignalStrength(0.023M, SignalStrengthLevel.High),
                MarketDirection.Bullish
            );
        var decisionId = decision.Id!.ToGuid();
        Mapper.ToDao(Arg.Any<Decision>()).Returns(new DecisionDao() { Id = decisionId });
        Mapper.ToDomain(Arg.Any<DecisionDao>()).Returns(decision);
        //Act
        var addResult = await _sut.Add(decision, CancellationToken.None);
        var getResult = await _sut.Get(decisionId, CancellationToken.None);
        //Assert
        addResult.IsSuccess.Should().BeTrue();
        getResult.IsSuccess.Should().BeTrue();
        getResult.Value.Should().NotBeNull();
        getResult.Value.Id?.Id.Should().Be(decisionId);
    }
}

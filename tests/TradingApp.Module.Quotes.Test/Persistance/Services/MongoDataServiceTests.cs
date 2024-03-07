using FluentAssertions;
using NSubstitute;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;
using TradingApp.Module.Quotes.Persistance.Mappers;
using TradingApp.Module.Quotes.Persistance.Models;
using TradingApp.Module.Quotes.Persistance.Services;
using TradingApp.TestUtils.Collections;
using TradingApp.TestUtils.Fixtures;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Persistance.Services;

[Collection(nameof(MongoDbFixtureCollection))]
public class MongoDataServiceTests : IClassFixture<MongoDbFixture>
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
    public async Task Execute_Success()
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

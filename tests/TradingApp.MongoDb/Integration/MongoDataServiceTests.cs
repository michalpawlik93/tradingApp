using NSubstitute;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;
using TradingApp.MongoDb.Adapters;
using TradingApp.MongoDb.Mappers;
using TradingApp.MongoDb.Models;

namespace TradingApp.MongoDb.Integration;

public class MongoDataServiceTests
{
    private readonly IMongoDbMapper<Decision, DecisionDao> Mapper = Substitute.For<IMongoDbMapper<Decision, DecisionDao>>();
    private readonly MongoDataService<Decision, DecisionDao> _sut;
    public MongoDataServiceTests()
    {
        _sut = new MongoDataService(Mapper);
    }


    [Fact]
    public async Task Add_Success()
    {
        //Arrange
        var indexOutcome = new IndexOutcome("RSI", 0.023M);
        var signalStrength = new SignalStrength(0.023M, SignalStrengthLevel.High);
        var decision = Decision.CreateNew(indexOutcome, DateTime.UtcNow, TradeAction.Buy, signalStrength, MarketDirection.Bullish);
        //Act
        var result = await _sut.Add(decision, CancellationToken.None);

        //Assert
        result.Messages.Should().NotBeEmpty();
    }
}

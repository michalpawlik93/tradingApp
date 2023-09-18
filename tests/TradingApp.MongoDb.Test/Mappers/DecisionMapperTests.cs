using FluentAssertions;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;
using TradingApp.MongoDb.Mappers;
using TradingApp.MongoDb.Models;

namespace TradingApp.MongoDb.Test.Mappers;

public class DecisionMapperTests
{
    private readonly IMongoDbMapper<Decision, DecisionDao> _sut;

    public DecisionMapperTests()
    {
        _sut = new DecisionMapper();
    }

    [Fact]
    public void ToDao_GetDomain_ReturnsDao()
    {
        //Arrange
        var indexOutcome = new IndexOutcome("RSI", 0.023M);
        var signalStrength = new SignalStrength(0.023M, SignalStrengthLevel.High);
        var decision = Decision.CreateNew(indexOutcome, DateTime.UtcNow, TradeAction.Buy, signalStrength, MarketDirection.Bullish);
        //Act
        var dao = _sut.ToDao(decision);
        //Assert
        dao.Should().NotBeNull();
    }

    [Fact]
    public void ToDomain_GetDao_ReturnsDomain()
    {
        //Arrange
        var decision = new DecisionDao()
        {
            Id = Guid.NewGuid(),
            Action = TradeAction.Buy.ToString(),
            IndexOutcomeName = "RSI",
            IndexOutcomeValue = 1.22M,
            MarketDirection = MarketDirection.Bullish.ToString(),
            TimeStamp = DateTime.UtcNow,
            SignalStrengthLevel = SignalStrengthLevel.High.ToString(),
            SignalStrengthValue = 1.23M,
        };
        //Act
        var domain = _sut.ToDomain(decision);
        //Assert
        domain.Should().NotBeNull();
    }
}

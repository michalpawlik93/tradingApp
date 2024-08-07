﻿using FluentAssertions;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;
using TradingApp.Module.Quotes.Persistance.Mappers;
using TradingApp.Module.Quotes.Persistance.Models;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Persistance.Mappers;

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
        var decision = Decision.CreateNew(indexOutcome, DateTime.UtcNow, TradeAction.Buy, MarketDirection.Bullish);
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
        };
        //Act
        var domain = _sut.ToDomain(decision);
        //Assert
        domain.Should().NotBeNull();
    }
}

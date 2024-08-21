using FluentAssertions;
using TradingApp.Module.Quotes.Application.Mappers;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Enums;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Mappers;

public class GetCombinedQuotesResponseMapperTests
{
    [Fact]
    public void ToDto_IncludeRsiFalse_ReturnsQuotesWithoutRsi()
    {
        // Arrange
        var quotes = new List<Quote> { new() };
        // Act
        var result = GetCombinedQuotesResponseMapper.ToDto(
            quotes,
            new List<RsiResult>(),
            new List<SrsiSignal>()
        );

        // Assert
        result.Quotes.Should().HaveCount(1);
        var resultQuotes = result.Quotes.ToList();
        resultQuotes[0].Rsi.Should().BeNull();
        resultQuotes[0].SrsiSignal.Should().BeNull();
        resultQuotes[0].Ohlc.Should().NotBeNull();
    }

    [Fact]
    public void ToDto_IncludeALlTrue_ReturnsQuotesWithIndicies()
    {
        // Arrange
        var quotes = new List<Quote> { new() };
        var rsiResults = new List<RsiResult> { new() { Value = 0.2m } };
        var srsiResults = new List<SrsiSignal>() { new(0.2m, 0.2m, TradeAction.Buy) };
        // Act
        var result = GetCombinedQuotesResponseMapper.ToDto(quotes, rsiResults, srsiResults);

        // Assert
        result.Quotes.Should().HaveCount(1);
        var resultQuotes = result.Quotes.ToList();
        resultQuotes[0].Rsi.Should().NotBeNull();
        resultQuotes[0].SrsiSignal.Should().NotBeNull();
        resultQuotes[0].Ohlc.Should().NotBeNull();
    }
}

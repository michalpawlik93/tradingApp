using FluentAssertions;
using TradingApp.Module.Quotes.Application.Mappers;
using TradingApp.Module.Quotes.Contract.Models;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Quotes.Application.Mappers;

public class GetCombinedQuotesResponseMapperTests
{
    [Fact]
    public void ToDto_IncludeRsiFalse_ReturnsQuotesWithoutRsi()
    {
        // Arrange
        var quotes = new List<Quote> { new() };
        var rsiResults = new List<RsiResult>();
        const bool includeRsi = false;
        // Act
        var result = GetCombinedQuotesResponseMapper.ToDto(quotes, rsiResults, includeRsi);

        // Assert
        result.Quotes.Should().HaveCount(1);
        var resultQuotes = result.Quotes.ToList();
        resultQuotes[0].Rsi.Should().BeNull();
        resultQuotes[0].Sma.Should().BeNull();
        resultQuotes[0].Ohlc.Should().NotBeNull();
        result.RsiSettings.Should().BeNull();
    }

    [Fact]
    public void ToDto_IncludeRsiTrue_ReturnsQuotesWithRsi()
    {
        // Arrange
        var quotes = new List<Quote> { new() };
        var rsiResults = new List<RsiResult> { new() { Value = 0.2m } };
        const bool includeRsi = true;
        // Act
        var result = GetCombinedQuotesResponseMapper.ToDto(quotes, rsiResults, includeRsi);

        // Assert
        result.Quotes.Should().HaveCount(1);
        var resultQuotes = result.Quotes.ToList();
        resultQuotes[0].Rsi.Should().NotBeNull();
        resultQuotes[0].Sma.Should().BeNull();
        resultQuotes[0].Ohlc.Should().NotBeNull();
        result.RsiSettings.Should().NotBeNull();
    }
}

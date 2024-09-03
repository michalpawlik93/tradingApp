using FluentAssertions;
using TradingApp.TingoProvider.Contract;
using TradingApp.TingoProvider.Contstants;
using TradingApp.TingoProvider.Mappers;
using Xunit;

namespace TradingApp.TingoProvider.Test.Mappers;

public class TingoQuoteMapperTests
{
    [Fact]
    public void MapToQuotes_TingoQuotes_ReturnsQuotesList()
    {
        // Arrange
        var tingoQuotes = new[]
        {
            new TingoQuote(
                Ticker.Curebtc,
                "baseCurrency",
                "quoteCurrency",
                [
                    new(
                        3914.7494m,
                        3914.7494m,
                        3914.7494m,
                        3914.7494m,
                        "2019-01-02T00:00:00+00:00",
                        756,
                        3914.7494m,
                        3914.7494m
                    )
                ]
            )
        };
        // Act
        var quotes = tingoQuotes.MapToQuotes().Value;

        // Assert
        quotes.Should().HaveCount(1);
        quotes[0].Open.Should().Be(3914.7494m);
        quotes[0].High.Should().Be(3914.7494m);
        quotes[0].Low.Should().Be(3914.7494m);
        quotes[0].Close.Should().Be(3914.7494m);
        quotes[0].Volume.Should().Be(3914.7494m);
    }

    [Fact]
    public void MapToQuotes_EmptyTingoQuotes_ReturnsEmptyList()
    {
        // Arrange
        var tingoQuotes = Array.Empty<TingoQuote>();
        // Act
        var quotes = tingoQuotes.MapToQuotes();

        // Assert
        quotes.Value.Should().HaveCount(0);
    }
}

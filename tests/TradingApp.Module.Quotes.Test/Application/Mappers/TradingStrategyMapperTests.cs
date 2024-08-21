using TradingApp.Module.Quotes.Application.Features.TradeStrategy;
using TradingApp.Module.Quotes.Application.Mappers;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Mappers;

public class TradingStrategyMapperTests
{
    [Theory]
    [InlineData("Scalping", TradingStrategy.Scalping)]
    [InlineData("DayTrading", TradingStrategy.DayTrading)]
    [InlineData("EmaAndStoch", TradingStrategy.EmaAndStoch)]
    public void Map_ValidStrategyString_ReturnsCorrectTradingStrategy(
        string input,
        TradingStrategy expected
    )
    {
        // Act
        var result = TradingStrategyMapper.Map(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("InvalidStrategy")]
    public void Map_InvalidStrategyString_ReturnsDefaultScalping(string input)
    {
        // Act
        var result = TradingStrategyMapper.Map(input);

        // Assert
        Assert.Equal(TradingStrategy.Scalping, result);
    }
}

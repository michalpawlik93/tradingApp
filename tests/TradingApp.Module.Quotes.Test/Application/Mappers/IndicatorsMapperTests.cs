using FluentAssertions;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Mappers;
using TradingApp.Module.Quotes.Contract.Constants;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Mappers;

public class IndicatorsMapperTests
{
    [Fact]
    public void ToDomainModel_Should_Return_Correct_Model_When_Valid_Input()
    {
        // Arrange

        // Act
        var result = IndicatorsMapper.ToDomainModel(new[]
        {
            new IndicatorsDto
            {
                TechnicalIndicator = "Rsi",
                SideIndicators = []
            },
            new IndicatorsDto
            {
                TechnicalIndicator = "Srsi",
                SideIndicators = ["Ema2x","SlowSrsi","SlowFastSrsi"]
            }
        });

        // Assert
        result.Should().HaveCount(2);
        result[0].TechnicalIndicator.Should().Be(TechnicalIndicator.Rsi);
        result[0].SideIndicators.Should().HaveCount(0);

        result[1].TechnicalIndicator.Should().Be(TechnicalIndicator.Srsi);
        result[1].SideIndicators.Should().HaveCount(3);


        result[1].SideIndicators.ToArray()[0].Should().Be(SideIndicator.SlowFastSrsi);
        result[1].SideIndicators.ToArray()[1].Should().Be(SideIndicator.SlowSrsi);
        result[1].SideIndicators.ToArray()[2].Should().Be(SideIndicator.Ema2x);
    }

    [Fact]
    public void ToDomainModel_Should_Handle_Invalid_Values()
    {
        // Arrange

        // Act
        var result = IndicatorsMapper.ToDomainModel(new[]
        {
            new IndicatorsDto
            {
                TechnicalIndicator = "no index",
                SideIndicators = ["no index"]
            },
        });

        // Assert
        result.Should().HaveCount(1);
        result[0].TechnicalIndicator.Should().Be(TechnicalIndicator.Rsi);
        result[0].SideIndicators.Should().HaveCount(1);
        result[0].SideIndicators.ToArray()[0].Should().Be(SideIndicator.Ema2x);

    }
}


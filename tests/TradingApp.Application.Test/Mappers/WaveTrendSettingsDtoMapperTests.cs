using FluentAssertions;
using TradingApp.Application.Mappers;
using TradingApp.Application.Models;
using Xunit;

namespace TradingApp.Application.Test.Mappers;

public class WaveTrendSettingsDtoMapperTests
{
    [Fact]
    public void ToDomainModel_Should_Return_Correct_Model()
    {
        // Arrange
        var dto = new WaveTrendSettingsDto
        {
            AverageLength = 1,
            ChannelLength = 2,
            MovingAverageLength = 3,
            Overbought = 4,
            Oversold = 5,
        };

        // Act
        var result = WaveTrendSettingsDtoMapper.ToDomainModel(dto);

        // Assert
        result.Should().NotBeNull();
        result.AverageLength.Should().Be(dto.AverageLength);
        result.ChannelLength.Should().Be(dto.ChannelLength);
        result.MovingAverageLength.Should().Be(dto.MovingAverageLength);
        result.RsiSettings.Overbought.Should().Be(dto.Overbought);
        result.RsiSettings.Oversold.Should().Be(dto.Oversold);
    }
}

﻿using FluentAssertions;
using TradingApp.Modules.Application.Dtos;
using TradingApp.Modules.Application.Mappers;
using Xunit;

namespace TradingApp.Modules.Test.Quotes.Application.Mappers;

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
        result.Overbought.Should().Be(dto.Overbought);
        result.Oversold.Should().Be(dto.Oversold);
    }
}

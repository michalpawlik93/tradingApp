using AutoFixture.Xunit2;
using FluentAssertions;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Mappers;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Mappers;

public class SRsiSettingsDtoMapperUniteTests
{

    [Theory]
    [AutoData]
    public void ToDomainModel_Should_Return_CorrectResult(SRsiSettingsDto dto)
    {
        // Arrange
        // Act
        var result = SRsiSettingsDtoMapper.ToDomainModel(dto);

        // Assert
        result.Should().NotBeNull();
        result.Enable.Should().Be(dto.Enable);
        result.ChannelLength.Should().Be(dto.ChannelLength);
        result.Overbought.Should().Be(dto.Overbought);
        result.Oversold.Should().Be(dto.Oversold);
        result.StochDSmooth.Should().Be(dto.StochDSmooth);
        result.StochKSmooth.Should().Be(dto.StochKSmooth);
    }
}

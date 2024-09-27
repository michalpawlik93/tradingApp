using AutoFixture.Xunit2;
using FluentAssertions;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Mappers;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Mappers;

public class SettingsMapperTests
{
    [Theory]
    [AutoData]
    public void ToDomainModel_Should_Return_CorrectResult(
        SrsiSettingsDto dto,
        RsiSettingsDto dto2,
        MfiSettingsDto dto3,
        WaveTrendSettingsDto dto4
    )
    {
        // Arrange
        // Act
        var result = SettingsMapper.ToDomainModel(
            new SettingsDto { SrsiSettings = dto, RsiSettings = dto2, MfiSettings = dto3, WaveTrendSettings = dto4 }
        );

        // Assert
        result.Should().NotBeNull();
        result.SrsiSettings.Should().NotBeNull();
        result.SrsiSettings?.Enabled.Should().Be(dto.Enabled);
        result.SrsiSettings?.ChannelLength.Should().Be(dto.ChannelLength);
        result.SrsiSettings?.Overbought.Should().Be(dto.Overbought);
        result.SrsiSettings?.Oversold.Should().Be(dto.Oversold);
        result.SrsiSettings?.StochDSmooth.Should().Be(dto.StochDSmooth);
        result.SrsiSettings?.StochKSmooth.Should().Be(dto.StochKSmooth);

        result.RsiSettings.Should().NotBeNull();
        result.RsiSettings?.Enabled.Should().Be(dto2.Enabled);
        result.RsiSettings?.ChannelLength.Should().Be(dto2.ChannelLength);
        result.RsiSettings?.Overbought.Should().Be(dto2.Overbought);
        result.RsiSettings?.Oversold.Should().Be(dto2.Oversold);

        result.MfiSettings.Should().NotBeNull();
        result.MfiSettings?.ScaleFactor.Should().Be(dto3.ScaleFactor);
        result.MfiSettings?.ChannelLength.Should().Be(dto3.ChannelLength);

        result.WaveTrendSettings.Should().NotBeNull();
        result.WaveTrendSettings?.AverageLength.Should().Be(dto4.AverageLength);
        result.WaveTrendSettings?.ChannelLength.Should().Be(dto4.ChannelLength);
        result.WaveTrendSettings?.MovingAverageLength.Should().Be(dto4.MovingAverageLength);
        result.WaveTrendSettings?.Overbought.Should().Be(dto4.Overbought);
        result.WaveTrendSettings?.Oversold.Should().Be(dto4.Oversold);
    }
}

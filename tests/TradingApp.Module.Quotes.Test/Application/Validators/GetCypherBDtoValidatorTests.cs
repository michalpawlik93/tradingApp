﻿using FluentAssertions;
using TradingApp.Domain.Modules.Constants;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;
using TradingApp.Module.Quotes.Application.Validators;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Domain.Constants;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Validators;

public class GetCypherBDtoValidatorTests
{
    [Fact]
    public void GetCypherBDtoValidator_ValidDataDto_ReturnsValid()
    {
        // Arrange
        var dto = new GetCypherBDto
        {
            Asset = new AssetDto
            {
                Name = nameof(AssetName.BTCUSD),
                Type = nameof(AssetType.Cryptocurrency)
            },
            MfiSettings = new MfiSettingsDto { ChannelLength = MfiSettingsConst.ChannelLength },
            SRsiSettings = new SRsiSettingsDto
            {
                StochDSmooth = SRsiSettingsConst.StochDSmooth,
                StochKSmooth = SRsiSettingsConst.StochKSmooth,
                ChannelLength = SRsiSettingsConst.ChannelLength,
                Enable = true,
                Overbought = SRsiSettingsConst.Overbought,
                Oversold = SRsiSettingsConst.Oversold
            },
            TimeFrame = new TimeFrameDto
            {
                StartDate = "2023-07-09T10:30:00.000Z",
                EndDate = "2023-08-09T10:30:00.000Z",
                Granularity = nameof(Granularity.Daily)
            },
            WaveTrendSettings = new WaveTrendSettingsDto
            {
                Overbought = WaveTrendSettingsConst.Overbought,
                Oversold = WaveTrendSettingsConst.Oversold,
                OverboughtLevel2 = WaveTrendSettingsConst.OverboughtLevel2,
                OversoldLevel2 = WaveTrendSettingsConst.OversoldLevel2,
                AverageLength = WaveTrendSettingsConst.AverageLength,
                ChannelLength = WaveTrendSettingsConst.ChannelLength,
                Enable = true,
                EnableVwap = false,
                MovingAverageLength = WaveTrendSettingsConst.MovingAverageLength
            }
        };

        var validator = new GetCypherBDtoValidator();

        // Act
        var results = validator.Validate(dto);

        // Assert  
        results.IsValid.Should().BeTrue();
    }

    [Fact]
    public void GetCypherBDtoValidator_MissingPropertiesDto_ReturnsInvalidValid()
    {
        // Arrange
        var dto = new GetCypherBDto
        {
            Asset = null,
            MfiSettings = null,
            SRsiSettings = null,
            TimeFrame = null,
            WaveTrendSettings = null
        };

        var validator = new GetCypherBDtoValidator();

        // Act
        var results = validator.Validate(dto);

        // Assert  
        results.IsValid.Should().BeFalse();
        results.Errors.Should().HaveCount(5);
    }
}


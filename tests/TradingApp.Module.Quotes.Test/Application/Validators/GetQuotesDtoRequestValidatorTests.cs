using FluentAssertions;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Validators;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Domain.Constants;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Validators;

public class GetQuotesDtoRequestValidatorTests
{
    [Fact]
    public void GetCypherBDtoValidator_ValidDataDto_ReturnsValid()
    {
        // Arrange
        var dto = new GetQuotesDtoRequest
        {
            Asset = new AssetDto
            {
                Name = nameof(AssetName.BTCUSD),
                Type = nameof(AssetType.Cryptocurrency)
            },
            TimeFrame = new TimeFrameDto
            {
                StartDate = "2023-07-09T10:30:00.000Z",
                EndDate = "2023-08-09T10:30:00.000Z",
                Granularity = nameof(Granularity.Daily)
            },
            Indicators = [new IndicatorsDto()
            {
                SideIndicators = [],
                TechnicalIndicator = nameof(TechnicalIndicator.Srsi)
            }],
            Settings = new SettingsDto()
            {
                SrsiSettings = new SrsiSettingsDto
                {
                    StochDSmooth = SRsiSettingsConst.StochDSmooth,
                    StochKSmooth = SRsiSettingsConst.StochKSmooth,
                    ChannelLength = SRsiSettingsConst.ChannelLength,
                    Enabled = true,
                    Overbought = SRsiSettingsConst.Overbought,
                    Oversold = SRsiSettingsConst.Oversold
                }
            }
        };

        var validator = new GetQuotesDtoRequestValidator();

        // Act
        var results = validator.Validate(dto);

        // Assert  
        results.IsValid.Should().BeTrue();
    }

    [Fact]
    public void GetQuotesDtoRequestValidator_InvalidDto_ReturnsInvalid()
    {
        // Arrange
        var dto = new GetQuotesDtoRequest
        {
            Indicators = [],
            Asset = null,
            TimeFrame = null,
        };

        var validator = new GetQuotesDtoRequestValidator();

        // Act
        var results = validator.Validate(dto);

        // Assert  
        results.IsValid.Should().BeFalse();
        results.Errors.Should().HaveCount(3);
    }
}


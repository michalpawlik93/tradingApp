using FluentAssertions;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Validators;
using TradingApp.Module.Quotes.Contract.Constants;
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
            StartDate = "2023-07-09T10:30:00.000Z",
            EndDate = "2023-08-09T10:30:00.000Z",
            Granularity = nameof(Granularity.Daily),
            AssetName = nameof(AssetName.BTCUSD),
            AssetType = nameof(AssetType.Cryptocurrency)
        };

        var validator = new GetQuotesDtoRequestValidator();

        // Act
        var results = validator.Validate(dto);

        // Assert  
        results.IsValid.Should().BeTrue();
    }

    [Fact]
    public void GetQuotesDtoRequestValidator_InvalidDto_ReturnsInvalidValid()
    {
        // Arrange
        var dto = new GetQuotesDtoRequest
        {
            TechnicalIndicators = [],
            StartDate = null,
            EndDate = null,
            Granularity = "unknown",
            AssetName = "unknown",
            AssetType = "unknown",
        };

        var validator = new GetQuotesDtoRequestValidator();

        // Act
        var results = validator.Validate(dto);

        // Assert  
        results.IsValid.Should().BeFalse();
        results.Errors.Should().HaveCount(5);
    }
}


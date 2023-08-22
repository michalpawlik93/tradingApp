using FluentAssertions;
using TradingApp.Modules.Application.Dtos;
using TradingApp.Modules.Application.Mappers;
using TradingApp.Modules.Domain.Enums;
using Xunit;

namespace TradingApp.Modules.Test.Quotes.Application.Mappers;

public class AssetMapperTests
{
    [Fact]
    public void ToDomainModel_Should_Return_Correct_Asset()
    {
        // Arrange
        var assetDto = new AssetDto
        {
            Name = "USDPLN",
            Type = "Currencies"
        };

        // Act
        var result = AssetDtoMapper.ToDomainModel(assetDto);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(AssetName.USDPLN);
        result.Type.Should().Be(AssetType.Currencies);
    }

    [Fact]
    public void ToDomainModel_Should_Return_Default_Asset_When_Parse_Fails()
    {
        // Arrange
        var assetDto = new AssetDto
        {
            Name = "InvalidAssetName",
            Type = "InvalidAssetType"
        };

        // Act
        var result = AssetDtoMapper.ToDomainModel(assetDto);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(AssetName.BTC);
        result.Type.Should().Be(AssetType.Cryptocurrency);
    }
}

using FluentAssertions;
using Microsoft.AspNetCore.Http;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.TingoProvider.Utils;
using Xunit;

namespace TradingApp.TingoProvider.Test.Utils;

public class AssetValidatorTests
{
    [Fact]
    public void IsValid_Success()
    {
        // Arrange
        var asset = new Asset(AssetName.BTCUSD, AssetType.Cryptocurrency);
        // Act
        var isValid = asset.IsValid(out var validationResult);

        // Assert
        validationResult.IsSuccess.Should().BeTrue();
        isValid.Should().BeTrue();
        validationResult.Errors.Should().HaveCount(0);
    }

    [Fact]
    public void IsValid_AssetTypeRule_Fail()
    {
        // Arrange
        var asset = new Asset(AssetName.BTCUSD, AssetType.Currencies);
        // Act
        var isValid = asset.IsValid(out var validationResult);

        // Assert
        validationResult.IsFailed.Should().BeTrue();
        isValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
        validationResult.Errors[0].Metadata["ErrorCode"].Should().Be(StatusCodes.Status400BadRequest);
        validationResult.Errors[0].Message.Should().Be(AssetValidator.AssetTypeRuleMessage);
    }

    [Fact]
    public void IsValid_AssetNameRule_Fail()
    {
        // Arrange
        var asset = new Asset(AssetName.ANC, AssetType.Cryptocurrency);
        // Act
        var isValid = asset.IsValid(out var validationResult);

        // Assert
        validationResult.IsFailed.Should().BeTrue();
        isValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
        validationResult.Errors[0].Metadata["ErrorCode"].Should().Be(StatusCodes.Status400BadRequest);
        validationResult.Errors[0].Message.Should().Be(AssetValidator.AssetNameRuleMessage);
    }
}


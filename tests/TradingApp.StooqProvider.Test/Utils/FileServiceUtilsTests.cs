using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.StooqProvider.Utils;

namespace TradingApp.StooqProvider.Test.Utils;

public class FileServiceUtilsTests
{
    [Theory]
    [InlineData(Granularity.Daily, "history\\Dailystooq.zip")]
    [InlineData(Granularity.Hourly, "history\\Hourlystooq.zip")]
    [InlineData(Granularity.FiveMins, "history\\FiveMinsstooq.zip")]
    public void GetZipFilePath_ShouldReturnCorrectFilePath(Granularity granularity, string expectedFilePath)
    {
        // Act
        var result = granularity.GetZipFilePath();

        // Assert
        Assert.Equal(expectedFilePath, result);
    }

    [Theory]
    [InlineData(Granularity.Daily, "daily/")]
    [InlineData(Granularity.Hourly, "hourly/")]
    [InlineData(Granularity.FiveMins, "5 min/")]
    public void GetGranularityPath_ShouldReturnCorrectPath(Granularity granularity, string expectedPath)
    {
        // Act
        var result = granularity.GetGranularityPath();

        // Assert
        Assert.Equal(expectedPath, result);
    }

    [Theory]
    [InlineData(AssetType.Cryptocurrency, "cryptocurrencies/")]
    [InlineData(AssetType.Currencies, "currencies/major/")]
    public void GetAssetTypePath_ShouldReturnCorrectPath(AssetType assetType, string expectedPath)
    {
        // Act
        var result = assetType.GetAssetTypePath();

        // Assert
        Assert.Equal(expectedPath, result);
    }

    [Theory]
    [InlineData(AssetName.ANC, "anc.v.txt")]
    [InlineData(AssetName.BTC, "btc.v.txt")]
    [InlineData(AssetName.USDPLN, "usdpln.txt")]
    public void GetAssetFileName_ShouldReturnCorrectFileName(AssetName assetName, string expectedFileName)
    {
        // Act
        var result = assetName.GetAssetFileName();

        // Assert
        Assert.Equal(expectedFileName, result);
    }

    [Theory]
    [InlineData(Granularity.Daily, AssetType.Currencies, AssetName.USDPLN, "data/daily/world/currencies/major/usdpln.txt")]
    [InlineData(Granularity.Hourly, AssetType.Cryptocurrency, AssetName.BTC, "data/hourly/world/cryptocurrencies/btc.v.txt")]
    [InlineData(Granularity.FiveMins, AssetType.Currencies, AssetName.ANC, "data/5 min/world/currencies/major/anc.v.txt")]
    public void AncvFilePath_ShouldReturnCorrectFilePath(Granularity granularity, AssetType assetType, AssetName assetName, string expectedFilePath)
    {
        // Act
        var result = FileServiceUtils.AncvFilePath(granularity, assetType, assetName);

        // Assert
        Assert.Equal(expectedFilePath, result);
    }

    [Theory]
    [InlineData(Granularity.Daily, "db/d/?b=d_world_txt")]
    [InlineData(Granularity.Hourly, "db/d/?b=h_world_txt")]
    public void FileLocation_ShouldReturnCorrectLocation(Granularity granularity, string expectedLocation)
    {
        // Act
        var result = FileServiceUtils.FileLocation(granularity);

        // Assert
        Assert.Equal(expectedLocation, result);
    }

    [Fact]
    public void FileLocation_ShouldThrowArgumentExceptionForInvalidGranularity()
    {
        // Arrange
        var invalidGranularity = (Granularity)999;

        // Act
        Action act = () => FileServiceUtils.FileLocation(invalidGranularity);

        // Assert
        Assert.Throws<ArgumentException>(act);
    }
}

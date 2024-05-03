using FluentAssertions;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.TingoProvider.Contstants;
using TradingApp.TingoProvider.Mappers;
using Xunit;

namespace TradingApp.TingoProvider.Test.Mappers;

public class TingoDataRequestMapperTests
{
    [Theory]
    [InlineData(AssetName.CUREBTC, Ticker.Curebtc)]
    [InlineData(AssetName.BTCUSD, Ticker.Btcusd)]
    [InlineData((AssetName)100, Ticker.Btcusd)]
    public void MapAsset_Asset_String(AssetName assetName, string expectedTicker)
    {
        // Arrange
        var asset = new Asset(assetName, AssetType.Cryptocurrency);
        // Act
        var assetString = asset.Map();

        // Assert
        assetString.Should().Be(expectedTicker);
    }

    [Theory]
    [InlineData(Granularity.FiveMins, ResambleFreq.FiveMin)]
    [InlineData(Granularity.Hourly, ResambleFreq.OneHour)]
    [InlineData(Granularity.Daily, ResambleFreq.OneDay)]
    [InlineData((Granularity)100, ResambleFreq.FiveMin)]
    public void Map_TimeFrame_TingoTimeFrame(Granularity granularity, string expectedGranularity)
    {
        // Arrange
        var timeFrame = new TimeFrame(granularity, null, null);
        // Act
        var tingoTimeFrame = timeFrame.Map();

        // Assert
        tingoTimeFrame.StartDate.Should().BeNull();
        tingoTimeFrame.EndDate.Should().BeNull();
        tingoTimeFrame.ResampleFreq.Should().Be(expectedGranularity);
    }
}


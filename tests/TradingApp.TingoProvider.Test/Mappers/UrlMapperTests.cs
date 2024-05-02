using FluentAssertions;
using TradingApp.Core.Utilities;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.TingoProvider.Contstants;
using TradingApp.TingoProvider.Mappers;
using Xunit;

namespace TradingApp.TingoProvider.Test.Mappers;

public class UrlMapperTests
{
    [Fact]
    public void GetCryptoQuotesUri_NullTimeFrame_ReturnsUrl()
    {
        // Arrange
        var timeFrame = new TimeFrame(Granularity.FiveMins, null, null);
        // Act
        var url = UrlMapper.GetCryptoQuotesUri(Ticker.Curebtc, timeFrame);

        // Assert
        url.Should()
            .Be($"tiingo/crypto/prices?tickers=curebtc&resampleFreq={ResambleFreq.FiveMin}");
    }

    [Fact]
    public void GetCryptoQuotesUri_AllParams_ReturnsUrl()
    {
        // Arrange
        var startDate = "2019-01-02T00:00:00+00:00";
        var endDate = "2020-01-02T00:00:00+00:00";
        var timeFrame = new TimeFrame(
            Granularity.FiveMins,
            DateTimeUtils.ConvertUtcIso8601_2DateStringToDateTime(startDate),
            DateTimeUtils.ConvertUtcIso8601_2DateStringToDateTime(endDate)
        );
        // Act
        var url = UrlMapper.GetCryptoQuotesUri(Ticker.Curebtc, timeFrame);

        // Assert
        url.Should()
            .Be(
                $"tiingo/crypto/prices?tickers=curebtc&startDate={startDate}&endDate={endDate}&resampleFreq={ResambleFreq.FiveMin}"
            );
    }
}

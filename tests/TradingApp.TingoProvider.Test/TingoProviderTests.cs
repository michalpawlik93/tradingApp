using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Net;
using TestUtils.Fixtures;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.TingoProvider.Contstants;
using TradingApp.TingoProvider.Setup;
using Xunit;

namespace TradingApp.TingoProvider.Test;

public class TingoProviderTests
{
    private readonly IOptions<TingoClientConfig> _options = Substitute.For<
        IOptions<TingoClientConfig>
    >();

    public TingoProviderTests()
    {
        _options.Value.Returns(
            new TingoClientConfig()
            {
                BaseUrl = "https://www.example.com/path?query=value",
                Token = "token"
            }
        );
    }

    [Fact]
    public async Task GetTickerMetadata_ShouldReturnSuccessResult()
    {
        // Arrange
        var sut = CreateProvider<MockTickerMetadataOk>();
        // Act
        var result = await sut.GetTickerMetadata(Ticker.Curebtc, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task GetTickerMetadata_InvalidTicker_ShouldReturnFailResult()
    {
        // Arrange
        var sut = CreateProvider<MockTickerMetadataOk>();
        // Act
        var result = await sut.GetTickerMetadata("ticker", CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public async Task GetTickerMetadata_ShouldReturnFailResult()
    {
        // Arrange
        var sut = CreateProvider<MockTickerMetadataFail>();
        // Act
        var result = await sut.GetTickerMetadata(Ticker.Curebtc, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public async Task GetQuotes_ShouldReturnSuccessResult()
    {
        // Arrange
        var sut = CreateProvider<MockQuotesOk>();
        // Act
        var result = await sut.GetQuotes(
            new TimeFrame(Granularity.FiveMins, null, null),
            new Asset(AssetName.CUREBTC, AssetType.Cryptocurrency),
            CancellationToken.None
        );

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    private TingoProvider CreateProvider<T>() where T : MockHttpMessageHandlerBase
    {
        var mockHttpMessageHandler = Substitute.ForPartsOf<T>();
        var tingoClient = Substitute.For<TingoClient>(
            new HttpClient(mockHttpMessageHandler),
            _options
        );
        return new TingoProvider(tingoClient);
    }
}

public class MockQuotesOk : MockHttpMessageHandlerBase
{
    protected override string HttpContent =>
        "[{\"ticker\":\"btcusd\",\"baseCurrency\":\"btc\",\"quoteCurrency\":\"usd\",\"priceData\":[{\"open\":3914.749407813885,\"high\":3942.374263716895,\"low\":3846.1755315352952,\"close\":3849.1217299601617,\"date\":\"2019-01-02T00\"}]}]";
    protected override HttpStatusCode StatusCode => HttpStatusCode.OK;
}

public class MockTickerMetadataOk : MockHttpMessageHandlerBase
{
    protected override string HttpContent =>
        "[{\"ticker\":\"curebtc\",\"name\":\"CureCoin (CURE/BTC)\",\"baseCurrency\":\"cure\",\"quoteCurrency\":\"btc\"}]";
    protected override HttpStatusCode StatusCode => HttpStatusCode.OK;
}

public class MockTickerMetadataFail : MockHttpMessageHandlerBase
{
    protected override string HttpContent => "";
    protected override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}

using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Net;
using TestUtils.Fixtures;
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

    private TingoProvider CreateProvider<T>() where T : MockHttpMessageHandlerBase
    {
        var mockHttpMessageHandler = Substitute.ForPartsOf<T>();
        var tingoClient = Substitute.For<TingoClient>(
            new HttpClient(mockHttpMessageHandler),
            _options
        );
        return new TingoProvider(tingoClient);
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
}

public class MockTickerMetadataOk : MockHttpMessageHandlerBase
{
    protected override string HttpContent => "[{\"ticker\":\"curebtc\",\"name\":\"CureCoin (CURE/BTC)\",\"baseCurrency\":\"cure\",\"quoteCurrency\":\"btc\"}]";
    protected override HttpStatusCode StatusCode => HttpStatusCode.OK;
}

public class MockTickerMetadataFail : MockHttpMessageHandlerBase
{
    protected override string HttpContent => "";
    protected override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}
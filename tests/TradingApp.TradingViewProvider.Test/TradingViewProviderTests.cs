using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Text.Json;
using TradingApp.TradingViewProvider.Constants;
using TradingApp.TradingViewProvider.Contract;
using TradingApp.TradingViewProvider.Setup;

namespace TradingApp.TradingViewProvider.Test;

public class TradingViewProviderTests
{
    private readonly IOptions<TradingViewClientConfig> Options = Substitute.For<
        IOptions<TradingViewClientConfig>
    >();
    private readonly HttpClient Client;
    private readonly ILogger<TradingViewProvider> Logger = Substitute.For<
        ILogger<TradingViewProvider>
    >();
    private readonly TradingViewClient TradingViewClient;
    private readonly TradingViewProvider _sut;

    public TradingViewProviderTests()
    {
        Options.Value.Returns(
            new TradingViewClientConfig() { BaseUrl = "https://www.example.com/path?query=value" }
        );
        var mockHttpMessageHandler = Substitute.ForPartsOf<MockHttpMessageHandler>();
        Client = new HttpClient(mockHttpMessageHandler);
        TradingViewClient = Substitute.For<TradingViewClient>(Client, Options);
        _sut = new TradingViewProvider(Logger, TradingViewClient);
    }

    [Fact]
    public async Task Authorize_ShouldReturnSuccessResult()
    {
        // Arrange
        var request = new TvAuthorizeRequest("login", "password", "locale");

        // Act
        var result = await _sut.Authorize(request);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Logout_ShouldReturnSuccessResult()
    {
        // Arrange
        // Act
        var result = await _sut.Logout();

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}

public class MockHttpMessageHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    ) => Task.FromResult(MockSend(request, cancellationToken));

    protected override HttpResponseMessage Send(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    ) => MockSend(request, cancellationToken);

    public virtual HttpResponseMessage MockSend(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    ) =>
        new HttpResponseMessage()
        {
            Content = new StringContent(
                JsonSerializer.Serialize(new ServiceResponseBase() { s = Status.OK })
            )
        };
}

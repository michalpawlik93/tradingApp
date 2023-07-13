using AutoFixture.NUnit3;
using Microsoft.Extensions.Logging;
using Moq;
using TradingApp.Application.Authorization.AuthorizeProvider;
using TradingApp.TradingAdapter.Models;
using TradingApp.TradingViewProvider;

namespace TradingApp.Application.Tests.Authorization.AuthorizeProvider;

[TestFixture]
public class AuthorizeProviderCommandHandlerTests
{
    private Mock<ITradingViewProvider> _mockTradingViewProvider;
    private Mock<ILogger<AuthorizeProviderCommandHandler>> _mockLogger;
    private AuthorizeProviderCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _mockTradingViewProvider = new Mock<ITradingViewProvider>();
        _mockLogger = new Mock<ILogger<AuthorizeProviderCommandHandler>>();
        _handler = new AuthorizeProviderCommandHandler(_mockTradingViewProvider.Object, _mockLogger.Object);
    }

    [Test]
    [AutoData]
    public async Task Handle_WithValidRequest_ReturnsServiceResponseWithNotNullResult(AuthorizeProviderCommand command, AuthorizeResponse result)
    {
        // Arrange
        _mockTradingViewProvider.Setup(provider => provider.Authorize(It.IsAny<AuthorizeRequest>()))
            .ReturnsAsync(result);

        // Act
        var response = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(response);
        Assert.IsNotNull(result);
    }
}

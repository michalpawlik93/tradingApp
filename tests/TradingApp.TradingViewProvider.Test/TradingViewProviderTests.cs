using Microsoft.Extensions.Logging;
using Moq;
using TradingApp.TradingViewProvider.Setup;

namespace TradingApp.TradingViewProvider.Test;

public class TradingViewProviderTests
{
    private readonly Mock<TradingViewClient> TradingViewClient = new();
    private readonly Mock<ILogger<TradingViewProvider>> Logger = new();
    private readonly TradingViewProvider _sut;

    public TradingViewProviderTests()
    {
        _sut = new TradingViewProvider(Logger.Object, TradingViewClient.Object);
    }

    //[Theory, AutoData]
    //public async Task Authorize_Success(AuthorizeRequest request)
    //{
    //    //Arrange
    //    //Act
    //    var result = await _sut.Authorize(request);
    //    //Assert
    //    result.IsSuccess.Should().BeTrue();
    //}
}
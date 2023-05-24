using TradingApp.TradingAdapter.Models;

namespace TradingApp.TradingViewProvider.Test;

public class TradingViewProviderTests
{
    [Theory, AutoData]
    public async Task Authorize_Success(AuthorizeRequest request, TradingViewProvider _sut)
    {
        //Arrange
        //Act
        var result = await _sut.Authorize(request);
        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}
using TradingApp.TradingViewProvider.Constants;
using TradingApp.TradingViewProvider.Contract;
using TradingApp.TradingViewProvider.Utils;

namespace TradingApp.TradingViewProvider.Test.Utils;

public class ServiceResponseUtilsTests
{
    [Theory, AutoData]
    public void GetResult_ServiceResponseBase_Fail(ServiceResponseBase serviceResponseBase)
    {
        //Arrange
        serviceResponseBase.s = Status.ERROR;
        //Act
        var result = serviceResponseBase.GetResult();
        //Assert
        result.IsFailed.Should().BeTrue();
    }

    [Theory, AutoData]
    public void GetResult_ServiceResponseBase_Ok(ServiceResponseBase serviceResponseBase)
    {
        //Arrange
        serviceResponseBase.s = Status.OK;
        //Act
        var result = serviceResponseBase.GetResult();
        //Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Theory, AutoData]
    public void GetResult_ServiceResponseBase_Fail_StatusNotFound(ServiceResponseBase serviceResponseBase)
    {
        //Arrange
        serviceResponseBase.s = "notFound";
        //Act
        var result = serviceResponseBase.GetResult();
        //Assert
        result.IsFailed.Should().BeTrue();
        result.Errors[0].Message.Should().Be("Status unknown received.");
    }
}

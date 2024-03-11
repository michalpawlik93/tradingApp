using System.Text.Json;
using TradingApp.TradingViewProvider.Constants;
using TradingApp.TradingViewProvider.Contract;
using TradingApp.TradingViewProvider.Utils;

namespace TradingApp.TradingViewProvider.Test.Utils;

public class ServiceResponseUtilsTests
{
    [Theory, AutoData]
    public void GetResultT_ServiceResponseT_Fail(ServiceResponse<string> serviceResponse)
    {
        //Arrange
        serviceResponse.s = Status.ERROR;
        //Act
        var result = serviceResponse.GetResult();
        //Assert
        result.IsFailed.Should().BeTrue();
    }

    [Theory, AutoData]
    public void GetResultT_ServiceResponseT_Ok(ServiceResponse<string> serviceResponse)
    {
        //Arrange
        serviceResponse.s = Status.OK;
        serviceResponse.d = "content";
        //Act
        var result = serviceResponse.GetResult();
        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be("content");
    }

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
    public void GetResult_ServiceResponseBase_Fail_StatusNotFound(
        ServiceResponseBase serviceResponseBase
    )
    {
        //Arrange
        serviceResponseBase.s = "notFound";
        //Act
        var result = serviceResponseBase.GetResult();
        //Assert
        result.IsFailed.Should().BeTrue();
        result.Errors[0].Message.Should().Be("Status unknown received.");
    }

    [Fact]
    public async Task DeserializeHttpResponse_ServiceResponseT()
    {
        //Arrange
        var expected = new ServiceResponse<string>() { s = Status.OK, d = "content" };
        var hettpResponse = new HttpResponseMessage();
        hettpResponse.StatusCode = System.Net.HttpStatusCode.OK;
        hettpResponse.Content = new StringContent(JsonSerializer.Serialize(expected));
        //Act
        var result = await ServiceResponseUtils.DeserializeHttpResponse<ServiceResponse<string>>(
            hettpResponse
        );
        //Assert
        result.Should().NotBeNull();
        result.d.Should().Be(expected.d);
    }

    [Fact]
    public async Task DeserializeHttpResponse_ServiceResponseBase()
    {
        //Arrange
        var expected = new ServiceResponseBase() { s = Status.OK };
        var hettpResponse = new HttpResponseMessage();
        hettpResponse.StatusCode = System.Net.HttpStatusCode.OK;
        hettpResponse.Content = new StringContent(JsonSerializer.Serialize(expected));
        //Act
        var result = await ServiceResponseUtils.DeserializeHttpResponse<ServiceResponseBase>(
            hettpResponse
        );
        //Assert
        result.Should().NotBeNull();
    }
}

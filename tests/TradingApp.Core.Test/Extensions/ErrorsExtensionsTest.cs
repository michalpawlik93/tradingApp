using FluentAssertions;
using FluentResults;
using TradingApp.Core.Enums;
using TradingApp.Core.Extensions;
using TradingApp.Core.Models;

namespace TradingApp.Core.Test.Extensions;

public class ErrorsExtensionsTest
{
    public static IEnumerable<object[]> TestData()
    {
        yield return [new BadRequestError("message"), MessageType.BadRequest];
        yield return [new SystemError("message"), MessageType.Error];
        yield return [new NotFoundError("message"), MessageType.NotFound];
        yield return [new Error("message"), MessageType.Error];
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void GetResultAsync_ReturnsFails(Error error, string expected)
    {
        // Arrange
        var fail = Result.Fail(error).Errors[0];
        //Act
        var result = fail.GetErrorServiceResponseMessage();
        //Assert
        result.Should().Be(expected);
    }
}


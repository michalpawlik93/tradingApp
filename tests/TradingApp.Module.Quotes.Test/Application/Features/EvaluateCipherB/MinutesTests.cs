using FluentAssertions;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Contract.Constants;
using Xunit;

namespace TradingApp.Module.Quotes.Test.Application.Features.EvaluateCipherB;

public class MinutesTests
{
    [Theory]
    [InlineData(Granularity.FiveMins, 5)]
    [InlineData(Granularity.Daily, 7200)]
    [InlineData(Granularity.Hourly, 300)]
    public void GetMaxSignalAge_GivenGranularity_MaxAgeReturned(
        Granularity granularity,
        int expectedAge
    )
    {
        //Arrange
        //Act
        var result = Minutes.GetMaxSignalAge(granularity);

        //Assert
        result.Errors.Should().BeEmpty();
        result.Value.Value.Should().Be(expectedAge);
    }

    [Fact]
    public void GetMaxSignalAge_Default_ErrorReturned()
    {
        //Arrange
        //Act
        var result = Minutes.GetMaxSignalAge((Granularity)5);

        //Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors[0].Message.Should().Be("Granularity out of scope. Enum does not exist: 5");
    }
}

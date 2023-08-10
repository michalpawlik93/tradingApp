using FluentAssertions;
using TradingApp.Modules.Quotes.Application.Mappers;
using TradingApp.Modules.Quotes.Application.Models;
using TradingApp.TradingAdapter.Enums;
using Xunit;

namespace TradingApp.Modules.Test.Quotes.Application.Mappers;

public class TimeFrameDtoMapperTests
{
    [Fact]
    public void ToDomainModel_Should_Return_Correct_Model()
    {
        // Arrange
        var dto = new TimeFrameDto
        {
            Granularity = "Daily",
            StartDate = "2023-07-12T10:30:00.000Z",
            EndDate = "2023-07-12T10:30:00.000Z"
        };

        // Act
        var result = TimeFrameDtoMapper.ToDomainModel(dto);

        // Assert
        result.Should().NotBeNull();
        result.Granularity.Should().Be(Granularity.Daily);
        result.StartDate.Should().NotBeNull();
        result.EndDate.Should().NotBeNull();
    }

    [Fact]
    public void ToDomainModel_Should_Return_Default_Model_When_Parse_Fails()
    {
        // Arrange
        var dto = new TimeFrameDto
        {
            Granularity = "invalid",
            StartDate = "invalid",
            EndDate = "invalid"
        };

        // Act
        var result = TimeFrameDtoMapper.ToDomainModel(dto);

        // Assert
        result.Should().NotBeNull();
        result.Granularity.Should().Be(Granularity.Hourly);
        result.StartDate.Should().BeNull();
        result.EndDate.Should().BeNull();
    }
}


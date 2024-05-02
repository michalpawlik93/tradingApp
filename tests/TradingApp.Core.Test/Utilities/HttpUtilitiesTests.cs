using FluentAssertions;
using System.Net;
using System.Text;
using System.Text.Json;
using TradingApp.Core.Utilities;

namespace TradingApp.Core.Tests.Utilities;

public class HttpUtilitiesTests
{
    [Fact]
    public async Task GetResultAsync_ReturnsFails()
    {
        // Arrange
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Accepted,
            Content = new StringContent("2")
        };
        //Act
        var result = await response.GetResultAsync<int>();
        //Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task GetResultAsync_ReturnsSuccess()
    {
        // Arrange
        var response = new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound };
        //Act
        var result = await response.GetResultAsync<int>();
        //Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task ConvertToHttpContent_SerializesObjectToJsonAndSetsContentTypeHeader()
    {
        // Arrange
        var model = new { Name = "John", Age = 30 };
        var expectedJson = JsonSerializer.Serialize(model);
        var expectedContent = new StringContent(expectedJson, Encoding.UTF8, "application/json");

        // Act
        var result = HttpUtilities.ConvertToHttpContent(model);

        // Assert
        result.Headers.ContentType.Should().NotBeNull();
        expectedContent.Headers.ContentType?.MediaType
            .Should()
            .Be(result.Headers.ContentType?.MediaType);
        var stringContent = await result.ReadAsStringAsync();
        expectedJson.Should().Be(stringContent);
    }

    [Fact]
    public async Task ConvertToUrlEncoded_ConvertsObjectToFormUrlEncodedContent()
    {
        // Arrange
        var model = new { Name = "John", Age = 30 };
        var expectedContent = new FormUrlEncodedContent(
            new Dictionary<string, string> { { "Name", "John" }, { "Age", "30" } }
        );

        // Act
        var result = HttpUtilities.ConvertToUrlEncoded(model);

        // Assert
        result.Headers.ContentType.Should().NotBeNull();
        expectedContent.Headers.ContentType?.MediaType
            .Should()
            .Be(result.Headers.ContentType?.MediaType);
        var stringContent = await result.ReadAsStringAsync();
        var expectedStringContent = await expectedContent.ReadAsStringAsync();
        expectedStringContent.Should().Be(stringContent);
    }
}

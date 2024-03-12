using FluentAssertions;
using System.Text;
using System.Text.Json;
using TradingApp.Core.Utilities;

namespace TradingApp.Core.Tests.Utilities;

public class HttpUtilitiesTests
{
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
        var strinContent = await result.ReadAsStringAsync();
        expectedJson.Should().Be(strinContent);
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
        var strinContent = await result.ReadAsStringAsync();
        var expectedStringContent = await expectedContent.ReadAsStringAsync();
        expectedStringContent.Should().Be(strinContent);
    }
}

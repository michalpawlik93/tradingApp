using System.Text;
using System.Text.Json;
using TradingApp.Common.Utilities;

namespace TradingApp.Common.Tests.Utilities;

public class HttpUtilitiesTests
{
    [Fact]
    public void ConvertToHttpContent_SerializesObjectToJsonAndSetsContentTypeHeader()
    {
        // Arrange
        var model = new { Name = "John", Age = 30 };
        var expectedJson = JsonSerializer.Serialize(model);
        var expectedContent = new StringContent(expectedJson, Encoding.UTF8, "application/json");

        // Act
        var result = HttpUtilities.ConvertToHttpContent(model);

        // Assert
        Assert.NotNull(result.Headers.ContentType);
        Assert.Equal(expectedContent.Headers.ContentType?.MediaType, result.Headers.ContentType?.MediaType);
        Assert.Equal(expectedJson, result.ReadAsStringAsync().Result);
    }

    [Fact]
    public void ConvertToUrlEncoded_ConvertsObjectToFormUrlEncodedContent()
    {
        // Arrange
        var model = new { Name = "John", Age = 30 };
        var expectedContent = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "Name", "John" },
            { "Age", "30" }
        });

        // Act
        var result = HttpUtilities.ConvertToUrlEncoded(model);

        // Assert
        Assert.NotNull(result.Headers.ContentType);
        Assert.Equal(expectedContent.Headers.ContentType?.MediaType, result.Headers.ContentType?.MediaType);
        Assert.Equal(expectedContent.ReadAsStringAsync().Result, result.ReadAsStringAsync().Result);
    }
}

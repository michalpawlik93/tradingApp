using FluentResults;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace TradingApp.Core.Utilities;

public static class HttpUtilities
{
    public static async Task<Result<T>> GetResultAsync<T>(this HttpResponseMessage response) =>
        response.IsSuccessStatusCode switch
        {
            false => Result.Fail($"Http request failed. StatusCode:{response.StatusCode}"),
            true => await response.GetHttpContentAsync<T>(),
        };

    public static ByteArrayContent ConvertToHttpContent(object model)
    {
        var jsonContent = JsonSerializer.Serialize(model);
        var buffer = Encoding.UTF8.GetBytes(jsonContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return byteContent;
    }

    public static FormUrlEncodedContent ConvertToUrlEncoded(object model)
    {
        var content = new FormUrlEncodedContent(ObjectToDictionary(model));
        content.Headers.Clear();
        content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        return content;
    }

    private static async Task<Result<T>> GetHttpContentAsync<T>(this HttpResponseMessage response)
    {
        var content = await response.Content.ReadFromJsonAsync<T>();
        return content != null
            ? Result.Ok(content)
            : Result.Fail($"Can not deserialize content to {typeof(T).Name}");
    }

    private static Dictionary<string, string> ObjectToDictionary(object obj)
    {
        var dict = new Dictionary<string, string>();

        foreach (var prop in obj.GetType().GetProperties())
        {
            var value = prop.GetValue(obj, null)?.ToString();
            if (!string.IsNullOrEmpty(value))
            {
                dict.Add(prop.Name, value);
            }
        }

        return dict;
    }
}

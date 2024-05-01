using FluentResults;
using System.Net.Http.Json;

namespace TradingApp.TingoProvider.Utils;

public static class HttpResposeUtils
{
    public static async Task<Result<T>> GetResultAsync<T>(this HttpResponseMessage response) =>
        response.IsSuccessStatusCode switch
        {
            false => Result.Fail($"Http request failed. StatusCode:{response.StatusCode}"),
            true => await response.GetHttpContentAsync<T>(),
        };

    private static async Task<Result<T>> GetHttpContentAsync<T>(this HttpResponseMessage response)
    {
        var content = await response.Content.ReadFromJsonAsync<T>();
        return content != null ? Result.Ok(content) : Result.Fail($"Can not deserialize content to {typeof(T).Name}");
    }
}

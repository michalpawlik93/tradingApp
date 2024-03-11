using FluentResults;
using System.Text.Json;
using TradingApp.TradingViewProvider.Constants;
using TradingApp.TradingViewProvider.Contract;

namespace TradingApp.TradingViewProvider.Utils;

public static class ServiceResponseUtils
{
    public static Result<T> GetResult<T>(this ServiceResponse<T> response) =>
        response.s switch
        {
            Status.ERROR => Result.Fail(response.errmsg),
            Status.OK => Result.Ok(response.d),
            _ => Result.Fail("Status unknown received.")
        };

    public static Result GetResult(this ServiceResponseBase response) =>
        response.s switch
        {
            Status.ERROR => Result.Fail(response.errmsg),
            Status.OK => Result.Ok(),
            _ => Result.Fail("Status unknown received.")
        };

    public static async Task<T> DeserializeHttpResponse<T>(HttpResponseMessage response)
    {
        var responseData = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<T>(responseData);
        ArgumentNullException.ThrowIfNull(result);
        return result;
    }
}

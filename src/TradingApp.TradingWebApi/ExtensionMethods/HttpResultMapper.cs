using FluentResults;
using TradingApp.Core.Models;

namespace TradingApp.TradingWebApi.ExtensionMethods;
public static class HttpResultMapper
{
    public static IResult MapToResult<T>(IResult<T> result)
    {
        if (!result.IsFailed) return Results.Ok(new ServiceResponse<T>(result));
        var res = (result as Result<T>);
        if (res != null && res.HasError<ValidationError>())
        {
            return Results.BadRequest(new ServiceResponse<T>(result));
        }
        if (res != null && res.HasError<NotFoundError>())
        {
            return Results.NotFound(new ServiceResponse<T>(result));
        }
        return Results.Problem(detail: result.Errors.FirstOrDefault()?.Message ?? "Unknown error");

    }

}


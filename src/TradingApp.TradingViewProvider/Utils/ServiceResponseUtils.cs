using FluentResults;
using TradingApp.TradingViewProvider.Constants;
using TradingApp.TradingViewProvider.Contract;

namespace TradingApp.TradingViewProvider.Utils;

public static class ServiceResponseUtils
{
    public static Result<TOutput> GetResult<TInput, TOutput>(this ServiceResponse<TInput> response, Func<TInput, TOutput> mapFunc)
    {
        if (response.s == Status.ERROR)
        {
            return Result.Fail(response.errmsg);
        }
        if (response.s == Status.OK)
        {
            return Result.Ok(mapFunc(response.d));
        }
        return Result.Fail("Status unknown received.");
    }

    public static Result GetResult(this ServiceResponseBase response)
    {
        if (response.s == Status.ERROR)
        {
            return Result.Fail(response.errmsg);
        }
        if (response.s == Status.OK)
        {
            return Result.Ok();
        }
        return Result.Fail("Status unknown received.");
    }
}

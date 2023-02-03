using FluentResults;
using TradingApp.Application.Abstraction;
using TradingApp.Application.Errors;
using TradingApp.Application.Models;

namespace TradingApp.Application.Services;

public class JwtProvider : IJwtProvider
{
    private static string GenerateTokenErrorMessage = "Error occured when generating token.";
    public Result<string> Generate(User user)
    {
        if(user.ApiKey is null || user.ApiSecret is null)
        {
            return Result.Fail<string>(GenerateTokenErrorMessage)
                .WithError(new UserError(user));
        }
        var token = "";
        return Result.Ok(token);
    }
}

using FluentResults;
using Microsoft.Extensions.Logging;
using TradingApp.Application.Abstraction;
using TradingApp.Application.Errors;
using TradingApp.Application.Models;

namespace TradingApp.Application.Services;

public class JwtProvider : IJwtProvider
{
    private static string GenerateTokenErrorMessage = "Error occured when generating token.";
    private readonly ILogger<JwtProvider> _logger;

    public JwtProvider(ILogger<JwtProvider> logger)
    {
        _logger = logger;
    }
    public Result<string> Generate(User user)
    {
        if(user.ApiKey is null || user.ApiSecret is null)
        {
            _logger.LogError(GenerateTokenErrorMessage);
            return Result.Fail<string>(GenerateTokenErrorMessage)
                .WithError(new UserError(user));
        }
        var token = "";
        return Result.Ok(token);
    }
}

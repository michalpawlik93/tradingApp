using FluentResults;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TradingApp.Modules.Abstraction;
using TradingApp.Modules.Authentication.Configuration;
using TradingApp.Modules.Errors;
using TradingApp.Modules.Models;

namespace TradingApp.Modules.Services;

public class JwtProvider : IJwtProvider
{
    private readonly ILogger<JwtProvider> _logger;
    private readonly IOptions<JwtOptions> _jwtOptions;

    public JwtProvider(ILogger<JwtProvider> logger, IOptions<JwtOptions> jwtOptions)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));
    }

    public Result<string> Generate(User user)
    {
        var useValidationResult = user.Validate();
        if (useValidationResult.IsFailed)
        {
            _logger.LogError(JwtProviderErrorMessages.GenerateTokenErrorMessage);
            return useValidationResult;
        }

        if (user.ApiSecret != _jwtOptions.Value.SecretKey)
        {
            _logger.LogError(JwtProviderErrorMessages.IncorrectCrednetialsErrorMessage);
            return Result
                .Fail<string>(JwtProviderErrorMessages.IncorrectCrednetialsErrorMessage)
                .WithError(new UserError());
        }

        return Result.Ok(GetToken());
    }

    private string GetToken()
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtTokenHandler.CreateToken(CreateSecurityTokenDescriptor);
        return jwtTokenHandler.WriteToken(token);
    }

    private SecurityTokenDescriptor CreateSecurityTokenDescriptor =>
        new()
        {
            Subject = new ClaimsIdentity(
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _jwtOptions.Value.Issuer),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }
            ),
            Expires = DateTime.UtcNow.AddHours(1),
            Audience = _jwtOptions.Value.Audience,
            Issuer = _jwtOptions.Value.Issuer,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SecretKey)),
                SecurityAlgorithms.HmacSha512
            )
        };
}

public static class JwtProviderErrorMessages
{
    public const string GenerateTokenErrorMessage = "Error occured when generating token.";
    public const string IncorrectCrednetialsErrorMessage = "Incorrect credentials.";
}

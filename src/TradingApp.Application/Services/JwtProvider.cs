using FluentResults;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TradingApp.Application.Abstraction;
using TradingApp.Application.Authentication.Configuration;
using TradingApp.Application.Errors;
using TradingApp.Application.Models;

namespace TradingApp.Application.Services;

public class JwtProvider : IJwtProvider
{
    private static string GenerateTokenErrorMessage = "Error occured when generating token.";
    private static string IncorrectCrednetialsErrorMessage = "Incorrect credentials.";
    private readonly ILogger<JwtProvider> _logger;
    private readonly IOptions<JwtOptions> _jwtOptions;

    public JwtProvider(ILogger<JwtProvider> logger, IOptions<JwtOptions> jwtOptions)
    {
        _logger = logger;
        _jwtOptions = jwtOptions;
    }
    public Result<string> Generate(User user)
    {
        if(user.Name is null || user.ApiSecret is null)
        {
            _logger.LogError(GenerateTokenErrorMessage);
            return Result.Fail<string>(GenerateTokenErrorMessage)
                .WithError(new UserError(user));
        }

        if(user.ApiSecret != _jwtOptions.Value.SecretKey)
        {
            _logger.LogError(IncorrectCrednetialsErrorMessage);
            return Result.Fail<string>(IncorrectCrednetialsErrorMessage)
                .WithError(new UserError(user));
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
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _jwtOptions.Value.Issuer),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Audience = _jwtOptions.Value.Audience,
            Issuer = _jwtOptions.Value.Issuer,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SecretKey)), SecurityAlgorithms.HmacSha512)
        };

}
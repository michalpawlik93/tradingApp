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
    private static string CredentialMissMatchErrorMessage = "Credentials missmatch.";
    private readonly ILogger<JwtProvider> _logger;
    private readonly IOptions<JwtOptions> _jwtOptions;

    public JwtProvider(ILogger<JwtProvider> logger, IOptions<JwtOptions> jwtOptions)
    {
        _logger = logger;
        _jwtOptions = jwtOptions;
    }
    public Result<string> Generate(User user)
    {
        if(user.ApiKey is null || user.ApiSecret is null)
        {
            _logger.LogError(GenerateTokenErrorMessage);
            return Result.Fail<string>(GenerateTokenErrorMessage)
                .WithError(new UserError(user));
        }

        if(user.ApiSecret != _jwtOptions.Value.SecretKey)
        {
            return Result.Fail<string>(CredentialMissMatchErrorMessage)
                .WithError(new UserError(user));
        }
        var issuer = _jwtOptions.Value.Issuer;
        var audience = _jwtOptions.Value.Audience;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SecretKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.ApiKey),
                new Claim(JwtRegisteredClaimNames.Email, user.ApiKey),
                // the JTI is used for our refresh token which we will be convering in the next video
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            // the life span of the token needs to be shorter and utilise refresh token to keep the user signedin
            // but since this is a demo app we can extend it to fit our current need
            Expires = DateTime.UtcNow.AddHours(6),
            Audience = audience,
            Issuer = issuer,
            // here we are adding the encryption alogorithim information which will be used to decrypt our token
            SigningCredentials = signingCredentials
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);

        return Result.Ok(jwtToken);
    }
}
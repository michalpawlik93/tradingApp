using MediatR;
using Microsoft.AspNetCore.Authorization;
using TradingApp.Modules.Authentication.GetToken;
using TradingApp.Modules.Models;

namespace TradingApp.TradingWebApi.Modules;

public static class AuthenticationModule
{
    public static void AddAuthenticationModule(this IEndpointRouteBuilder app)
    {
        app.MapPost("/authentication", [AllowAnonymous] async (User model, IMediator medaitor) =>
        {
            var response = await medaitor.Send(new GetTokenCommand(model));
            return Results.Ok(response);
        })
            .WithName("Issue Token")
            .WithOpenApi();
    }
}

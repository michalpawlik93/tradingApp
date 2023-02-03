using MediatR;
using TradingApp.Application.Authentication.GetToken;
using TradingApp.Application.Models;

namespace TradingApp.TradingWebApi.Modules;

public static class AuthenticationModule
{
    public static void AddAuthenticationModule(this IEndpointRouteBuilder app)
    {
        app.MapPost("/authentication", async (User model, IMediator medaitor) =>
        {
            var response = await medaitor.Send(new GetTokenCommand(model));
            return Results.Ok(response);
        })
            .WithName("Issue Token")
            .WithOpenApi();
    }
}

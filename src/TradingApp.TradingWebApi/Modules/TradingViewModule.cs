using MediatR;
using Microsoft.AspNetCore.Authorization;
using TradingApp.Application.Authentication.GetToken;
using TradingApp.Application.Models;

namespace TradingApp.TradingWebApi.Modules;

public static class TradingViewModule
{
    public static void AddTradingViewModule(this IEndpointRouteBuilder app)
    {
        app.MapPost("/tradingview/authorize", [AllowAnonymous] async (User model, IMediator medaitor) =>
        {
            var response = await medaitor.Send(new GetTokenCommand(model));
            return Results.Ok(response);
        })
            .WithName("Authorize Trading View")
            .WithOpenApi();
    }
}

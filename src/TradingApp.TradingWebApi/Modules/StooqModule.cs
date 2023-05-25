using MediatR;
using Microsoft.AspNetCore.Authorization;
using TradingApp.Application.Quotes.GetStooqQuotesTest;

namespace TradingApp.TradingWebApi.Modules;

public static class StooqModule
{
    public static void AddStooqModule(this IEndpointRouteBuilder app)
    {
        app.MapGet("/stooq/quote/getall", [AllowAnonymous] async (IMediator medaitor) =>
        {
            var response = await medaitor.Send(new GetStooqQuotesTestCommand());
            return Results.Ok(response);
        })
            .WithName("Get quotes Trading View")
            .WithOpenApi();
    }
}

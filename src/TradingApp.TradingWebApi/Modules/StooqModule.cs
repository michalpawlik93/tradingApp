using MediatR;
using Microsoft.AspNetCore.Authorization;
using TradingApp.Application.Quotes.GetStooqQuotes;

namespace TradingApp.TradingWebApi.Modules;

public static class StooqModule
{
    public static void AddStooqModule(this IEndpointRouteBuilder app)
    {
        app.MapGet("/stooq/combinedquote/getall", [AllowAnonymous] async (IMediator medaitor) =>
        {
            var response = await medaitor.Send(new GetStooqCombinedQuotesCommand());
            return Results.Ok(response);
        })
            .WithName("Get combined quotes Trading View")
            .WithOpenApi();
    }
}

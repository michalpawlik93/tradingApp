using MediatR;
using Microsoft.AspNetCore.Authorization;
using TradingApp.Application.Quotes.GetCypherB;
using TradingApp.Application.Quotes.GetStooqQuotes;
using TradingApp.TradingAdapter.Enums;

namespace TradingApp.TradingWebApi.Modules;

public static class StooqModule
{
    public static void AddStooqModule(this IEndpointRouteBuilder app)
    {
        app.MapGet("/stooq/combinedquote/getall", [AllowAnonymous] async (string granularity, IMediator medaitor) =>
        {

            Enum.TryParse<HistoryType>(granularity, out var granularityParsed);
            var response = await medaitor.Send(new GetStooqCombinedQuotesCommand(granularityParsed));
            return Results.Ok(response);
        })
            .WithName("Get combined quotes of stoqq")
            .WithOpenApi();
        app.MapGet("/stooq/cypherb", [AllowAnonymous] async (string granularity, IMediator medaitor) =>
        {

            Enum.TryParse<HistoryType>(granularity, out var granularityParsed);
            var response = await medaitor.Send(new GetCypherBCommand(granularityParsed, null, null));
            return Results.Ok(response);
        })
        .WithName("Get cypherb for stoqq data")
        .WithOpenApi();
    }
}

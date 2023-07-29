using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingApp.Modules.Quotes.GetCypherB;
using TradingApp.Modules.Quotes.GetCypherB.Dto;
using TradingApp.Modules.Quotes.GetStooqQuotes;
using TradingApp.Modules.Quotes.Models;

namespace TradingApp.TradingWebApi.Modules;

public static class StooqModule
{
    public static void AddStooqModule(this IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/stooq/combinedquote/getall",
                [AllowAnonymous]
        async ([AsParameters] GetQuotesDtoRequest request, IMediator medaitor) =>
                {
                    var response = await medaitor.Send(
                        GetStooqCombinedQuotesCommandExtensions.CreateCommandRequest(
                            request.Granularity,
                            request.AssetType,
                            request.AssetName,
                            request.StartDate,
                            request.EndDate
                        )
                    );
                    return Results.Ok(response);
                }
            )
            .WithName("Get combined quotes of stoqq")
            .WithOpenApi();

        app.MapPost(
                "/stooq/cypherb",
                [AllowAnonymous]
        async ([FromBody] GetCypherBDto request, IMediator mediator) =>
                {
                    var response = await mediator.Send(request.CreateCommand());
                    return Results.Ok(response);
                }
            )
            .WithName("Get cypherb for stoqq data")
            .WithOpenApi();
    }
}

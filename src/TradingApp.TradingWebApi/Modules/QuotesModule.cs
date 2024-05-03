using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes;
using TradingApp.Module.Quotes.Application.Features.GetCypherB;
using TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;
using TradingApp.Module.Quotes.Application.Features.TickerMetadata;

namespace TradingApp.TradingWebApi.Modules;

public static class QuotesModule
{
    public static void AddQuotesModule(this IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/tingo/tickermetadata",
                [AllowAnonymous]
        async ([AsParameters] GetTickerMetadataDto request, IMediator mediator) => Results.Ok(await mediator.Send(
            new GetTickerMetadataQuery(request.Ticker)
        )))
            .WithName("Get ticker metadata")
            .WithOpenApi();

        app.MapGet(
                "/stooq/combinedquote/getall",
                [AllowAnonymous]
        async ([AsParameters] GetQuotesDtoRequest request, IMediator mediator) =>
                {
                    var response = await mediator.Send(
                        GetCombinedQuotesCommandExtensions.CreateCommandRequest(
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
            .WithDescription("Get quotes after evaluation")
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

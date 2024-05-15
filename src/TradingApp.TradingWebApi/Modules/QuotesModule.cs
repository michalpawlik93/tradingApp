﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes;
using TradingApp.Module.Quotes.Application.Features.GetCypherB;
using TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;
using TradingApp.Module.Quotes.Application.Features.TickerMetadata;
using TradingApp.TradingWebApi.ExtensionMethods;

namespace TradingApp.TradingWebApi.Modules;

public static class QuotesModule
{
    public static void AddQuotesModule(this IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/quotes/tingo/tickermetadata",
                [AllowAnonymous]
        async ([AsParameters] AssetAsParamsDto request, IMediator mediator) =>
                    HttpResultMapper.MapToResult(
                        await mediator.Send(
                            GetTickerMetadataQueryExtensions.CreateCommandRequest(request)
                        )
                    )
            )
            .WithName("Get ticker metadata")
            .WithOpenApi();

        app.MapGet(
                "/quotes/combinedquotes",
                [AllowAnonymous]
        async ([AsParameters] GetQuotesDtoRequest request, IMediator mediator) =>
                    HttpResultMapper.MapToResult(
                        await mediator.Send(
                            GetCombinedQuotesCommandExtensions.CreateCommandRequest(request)
                        )
                    )
            )
            .WithName("Get combined quotes")
            .WithDescription(
                "Get quotes after evaluation. Specify time frame and type of technical indicies"
            )
            .WithOpenApi();

        app.MapPost(
                "/quotes/cypherb",
                [AllowAnonymous]
        async ([FromBody] GetCypherBDto request, IMediator mediator) =>
                    HttpResultMapper.MapToResult(await mediator.Send(request.CreateCommand()))
            )
            .WithName("Get cypherb technical indicator for quotes in time range")
            .WithOpenApi();
    }
}
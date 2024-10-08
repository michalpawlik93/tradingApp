﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes;
using TradingApp.Module.Quotes.Application.Features.GetCypherB;
using TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;
using TradingApp.Module.Quotes.Application.Features.TickerMetadata;
using TradingApp.Module.Quotes.Application.Validators;
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

        app.MapPost(
                "/quotes/combinedquotes",
                [AllowAnonymous]
        async ([FromBody] GetQuotesDtoRequest request, IMediator mediator) =>
                {
                    var commandResult = request.CreateCommand();

                    return commandResult.IsFailed
                        ? HttpResultMapper.MapToResult(commandResult)
                        : HttpResultMapper.MapToResult(await mediator.Send(commandResult.Value));
                }
            )
            .AddEndpointFilter<ValidatorFilter<GetQuotesDtoRequest>>()
            .WithName("Get combined quotes")
            .WithDescription(
                "Get quotes after evaluation. Specify time frame and type of technical indices"
            )
            .WithOpenApi();

        app.MapPost(
                "/quotes/cypherb",
                [AllowAnonymous]
        async ([FromBody] GetCypherBDto request, IMediator mediator) =>
                {
                    var commandResult = request.CreateCommand();
                    return commandResult.IsFailed
                        ? HttpResultMapper.MapToResult(commandResult)
                        : HttpResultMapper.MapToResult(await mediator.Send(commandResult.Value));
                }
            )
            .AddEndpointFilter<ValidatorFilter<GetCypherBDto>>()
            .WithName("Get cypherb technical indicator for quotes in time range")
            .WithOpenApi();
    }
}

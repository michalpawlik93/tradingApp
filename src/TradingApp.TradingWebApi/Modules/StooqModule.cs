using MediatR;
using Microsoft.AspNetCore.Authorization;
using TradingApp.Application.Models;
using TradingApp.Application.Quotes.GetCypherB;
using TradingApp.Application.Quotes.GetStooqQuotes;
using TradingApp.Common.Utilities;
using TradingApp.TradingAdapter.Enums;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.TradingWebApi.Modules;

public static class StooqModule
{
    public static void AddStooqModule(this IEndpointRouteBuilder app)
    {
        app.MapGet(
                "/stooq/combinedquote/getall",
                [AllowAnonymous]
        async (
                    [AsParameters] GetQuotesDtoRequest request,
                    IMediator medaitor
                ) =>
                {
                    var (timeFrame, asset) = CreateCommandRequest(
                        request.Granularity,
                        request.AssetType,
                        request.AssetName,
                        request.StartDate,
                        request.EndDate
                    );
                    var response = await medaitor.Send(
                        new GetStooqCombinedQuotesCommand(timeFrame, asset)
                    );
                    return Results.Ok(response);
                }
            )
            .WithName("Get combined quotes of stoqq")
            .WithOpenApi();
        app.MapGet(
                "/stooq/cypherb",
                [AllowAnonymous]
        async (
                    [AsParameters] GetQuotesDtoRequest request,
                    IMediator medaitor
                ) =>
                {
                    var (timeFrame, asset) = CreateCommandRequest(
                        request.Granularity,
                        request.AssetType,
                        request.AssetName,
                        request.StartDate,
                        request.EndDate
                    );
                    var response = await medaitor.Send(new GetCypherBCommand(timeFrame, asset));
                    return Results.Ok(response);
                }
            )
            .WithName("Get cypherb for stoqq data")
            .WithOpenApi();
    }

    private static (TimeFrame timeFrame, Asset asset) CreateCommandRequest(
        string granularity,
        string assetType,
        string assetName,
        string startDate,
        string endDate
    )
    {
        Enum.TryParse<Granularity>(granularity, out var granularityParsed);
        Enum.TryParse<AssetName>(assetName, out var assetNameParsed);
        Enum.TryParse<AssetType>(assetType, out var assetTypeParsed);
        return (
            new TimeFrame(granularityParsed, DateTimeUtils.ParseIso8601DateString(startDate), DateTimeUtils.ParseIso8601DateString(endDate)),
            new Asset(assetNameParsed, assetTypeParsed)
        );
    }
}

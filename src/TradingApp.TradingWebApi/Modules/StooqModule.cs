using MediatR;
using Microsoft.AspNetCore.Authorization;
using TradingApp.Application.Quotes.GetStooqQuotes;
using TradingApp.TradingAdapter.Enums;

namespace TradingApp.TradingWebApi.Modules;

public static class StooqModule
{
    public static void AddStooqModule(this IEndpointRouteBuilder app)
    {
        app.MapGet("/stooq/combinedquote/getall", [AllowAnonymous] async (string historyType, IMediator medaitor) =>
        {

            Enum.TryParse<HistoryType>(historyType, out var historyTypeParsed);
            var response = await medaitor.Send(new GetStooqCombinedQuotesCommand(historyTypeParsed));
            return Results.Ok(response);
        })
            .WithName("Get combined quotes of stoqq")
            .WithOpenApi();
    }
}

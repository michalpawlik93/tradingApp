using MediatR;

namespace TradingApp.TradingWebApi.Modules;

public static class TestModule
{
    public static void AddTestModule(this IEndpointRouteBuilder app)
    {
        app.MapGet("/test", (IMediator medaitor) =>
        {
            return Results.Ok("Test success");
        })
            .WithName("Test Endpoint")
            .WithOpenApi()
            .RequireAuthorization();
    }
}

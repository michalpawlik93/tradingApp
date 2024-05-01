namespace TradingApp.TradingWebApi.Modules;

public static class TestModule
{
    public static void AddTestModule(this IEndpointRouteBuilder app)
    {
        app.MapGet("/test", () => Results.Ok("Test success"))
            .WithName("Test Endpoint")
            .WithOpenApi()
            .RequireAuthorization();
    }
}

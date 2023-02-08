using MediatR;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog;
using TradingApp.Application.Abstraction;
using TradingApp.Application.Services;
using TradingApp.Application.Authentication.GetToken;
using TradingApp.Application.Models;

namespace TradingApp.TradingWebApi.ExtensionMethodes;

public static class ServicesExtensionMethods
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(Mediator));
        services.AddScoped<IRequestHandler<GetTokenCommand, ServiceResponse<string>>, GetTokenCommandHandler>();
        services.AddTransient<IJwtProvider, JwtProvider>();
    }

    public static void AddLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(new LoggerConfiguration()
            .WriteTo.Console(new JsonFormatter())
            .WriteTo.File(new JsonFormatter(), "TradingWebApi_Log.txt")
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .CreateLogger());
    }
}

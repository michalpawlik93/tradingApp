using MediatR;
using TradingApp.Application.Abstraction;
using TradingApp.Application.Services;

namespace TradingApp.TradingWebApi.ExtensionMethodes;

public static class ServicesExtensionMethods
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ServicesExtensionMethods).Assembly);
        services.AddTransient<IJwtProvider, JwtProvider>();
    }
}

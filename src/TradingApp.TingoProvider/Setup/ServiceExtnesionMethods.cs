using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace TradingApp.TingoProvider.Setup;

[ExcludeFromCodeCoverage]
public static class ServiceExtnesionMethods
{
    public static void AddTingoProvider(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TingoClientConfig>(configuration.GetSection(TingoClientConfig.ConfigSectionName));
        services.AddHttpClient<TingoClient>();
        services.AddSingleton<ITingoProvider, TingoProvider>();
    }
}



using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using TradingApp.TradingViewProvider.Abstraction;


namespace TradingApp.TradingViewProvider.Setup;

[ExcludeFromCodeCoverage]
public static class ServicesExtensionMethods
{
    public static void AddTradingViewProvider(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TradingViewClientConfig>(configuration.GetSection(TradingViewClientConfig.ConfigSectionName));
        services.AddHttpClient<TradingViewClient>();
        services.AddSingleton<ITradingViewProvider, TradingViewProvider>();
    }
}

[ExcludeFromCodeCoverage]
public class TradingViewClientConfig
{
    public const string ConfigSectionName = "TradingViewClient";
    public string? BaseUrl { get; set; }
}

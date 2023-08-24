using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TradingApp.Module.Quotes.Ports;

namespace TradingApp.TradingViewProvider.Setup;

public static class ServicesExtensionMethods
{
    public static void AddTradingViewProvider(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TradingViewClientConfig>(configuration.GetSection(TradingViewClientConfig.ConfigSectionName));
        services.AddHttpClient<TradingViewClient>();
        services.AddSingleton<ITradingAdapter, TradingViewProvider>();
    }
}

public class TradingViewClientConfig
{
    public const string ConfigSectionName = "TradingViewClient";
    public string? BaseUrl { get; set; }
}

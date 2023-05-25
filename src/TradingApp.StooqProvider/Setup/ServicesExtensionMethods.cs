using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TradingApp.StooqProvider.Setup;
public static class ServicesExtensionMethods
{
    public static void AddTradingViewProvider(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StooqClientConfig>(configuration.GetSection(StooqClientConfig.ConfigSectionName));
        services.AddHttpClient<StooqClient>();
        services.AddSingleton<IStooqProvider, StooqProvider>();
    }
}

public class StooqClientConfig
{
    public const string ConfigSectionName = "StooqClient";
    public string BaseUrl { get; set; }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using TradingApp.Module.Quotes.Ports;
using TradingApp.StooqProvider.Abstraction;
using TradingApp.StooqProvider.Services;

namespace TradingApp.StooqProvider.Setup;

[ExcludeFromCodeCoverage]
public static class ServicesExtensionMethods
{
    public static void AddStooqProvider(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StooqClientConfig>(configuration.GetSection(StooqClientConfig.ConfigSectionName));
        services.AddHttpClient<StooqClient>();
        services.AddSingleton<IZipArchiveProvider, ZipArchiveProvider>();
        services.AddSingleton<IFileService, FileService>();
        services.AddSingleton<ITradingAdapter, StooqProvider>();
    }
}

public class StooqClientConfig
{
    public const string ConfigSectionName = "StooqClient";
    public string? BaseUrl { get; set; }
}

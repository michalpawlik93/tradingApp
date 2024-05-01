using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using TradingApp.StooqProvider.Abstraction;
using TradingApp.StooqProvider.Services;

namespace TradingApp.StooqProvider.Setup;

[ExcludeFromCodeCoverage]
public static class ServicesExtensionMethods
{
    public static void AddStooqProvider(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IZipArchiveProvider, ZipArchiveProvider>();
        services.AddSingleton<IFileService, FileService>();
        services.AddSingleton<IStooqProvider, StooqProvider>();
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System.Diagnostics.CodeAnalysis;
using TradingApp.Module.Quotes.Authentication.Abstraction;
using TradingApp.Module.Quotes.Authentication.Services;
using TradingApp.Module.Quotes.Configuration;
using TradingApp.TradingWebApi.Modules;


namespace TradingApp.TradingWebApi.ExtensionMethods;

[ExcludeFromCodeCoverage]
public static class ServicesExtensionMethods
{
    public static void AddModules(this WebApplication webApplication)
    {
        webApplication.AddAuthenticationModule();
        webApplication.AddTestModule();
        webApplication.AddQuotesModule();
    }
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IJwtProvider, JwtProvider>();
        services.AddQuotesServices(configuration);
    }

    public static void AddLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(
            new LoggerConfiguration().WriteTo
                .Console(new JsonFormatter())
                .WriteTo.File(new JsonFormatter(), "TradingWebApi_Log.txt")
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .CreateLogger()
        );
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            var securityReq = new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    new string[] { }
                }
            };

            setup.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, jwtSecurityScheme);
            setup.AddSecurityRequirement(securityReq);
        });
    }
}

using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using TradingApp.Application.Abstraction;
using TradingApp.Application.Authentication.GetToken;
using TradingApp.Application.Models;
using TradingApp.Application.Services;
using TradingApp.StooqProvider.Setup;

namespace TradingApp.TradingWebApi.ExtensionMethodes;

public static class ServicesExtensionMethods
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(typeof(Mediator));
        services.AddScoped<IRequestHandler<GetTokenCommand, ServiceResponse<string>>, GetTokenCommandHandler>();
        services.AddTransient<IJwtProvider, JwtProvider>();
        services.AddStooqProvider(configuration);
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
                    new string[] {}
                }
            };

            setup.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, jwtSecurityScheme);
            setup.AddSecurityRequirement(securityReq);
        });
    }
}

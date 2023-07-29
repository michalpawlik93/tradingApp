using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using TradingApp.Core.Models;
using TradingApp.Modules.Authentication.Abstraction;
using TradingApp.Modules.Authentication.GetToken;
using TradingApp.Modules.Authentication.Services;
using TradingApp.Modules.Quotes.GetCypherB;
using TradingApp.Modules.Quotes.GetCypherB.Dto;
using TradingApp.Modules.Quotes.GetStooqCombinedQuotes.Dto;
using TradingApp.Modules.Quotes.GetStooqQuotes;
using TradingApp.StooqProvider.Setup;
using TradingApp.TradingAdapter.Evaluator;
using TradingApp.TradingAdapter.Interfaces;

namespace TradingApp.TradingWebApi.ExtensionMethodes;

public static class ServicesExtensionMethods
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(typeof(Mediator));
        services.AddScoped<
            IRequestHandler<GetTokenCommand, ServiceResponse<string>>,
            GetTokenCommandHandler
        >();
        services.AddScoped<
            IRequestHandler<
                GetStooqCombinedQuotesCommand,
                ServiceResponse<GetStooqCombinedQuotesResponseDto>
            >,
            GetStooqCombinedQuotesCommandHandler
        >();
        services.AddScoped<
            IRequestHandler<GetCypherBCommand, ServiceResponse<GetCypherBResponseDto>>,
            GetCypherBCommandHandler
        >();
        services.AddTransient<IJwtProvider, JwtProvider>();
        services.AddTransient<IEvaluator, CustomEvaluator>();
        services.AddStooqProvider(configuration);
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

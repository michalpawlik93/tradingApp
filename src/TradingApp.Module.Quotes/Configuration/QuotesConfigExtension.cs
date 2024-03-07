using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using TradingApp.Core.EventBus;
using TradingApp.Core.Extensions;
using TradingApp.Core.Models;
using TradingApp.Evaluator;
using TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;
using TradingApp.Module.Quotes.Application.Features.GetCypherB;
using TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;
using TradingApp.Module.Quotes.Application.Features.GetStooqCombinedQuotes;
using TradingApp.Module.Quotes.Application.Features.GetStooqCombinedQuotes.Dto;
using TradingApp.Module.Quotes.Application.Services;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Persistance.Extensions;
using TradingApp.StooqProvider.Setup;

namespace TradingApp.Module.Quotes.Configuration;

[ExcludeFromCodeCoverage]
public static class QuotesConfigExtension
{
    public static void AddQuotoesServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddTransient<IEventBus, EventBus>();
        services.AddDomainEventBus();
        services.AddIntegrationEventBus(configuration);
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
        services.AddScoped<
            IRequestHandler<EvaluateSRsiCommand, ServiceResponse>,
            EvaluateSRsiCommandHandler
        >();

        services.AddTransient<IEvaluator, CustomEvaluator>();
        services.AddStooqProvider(configuration);
        services.AddMongoDbService(configuration);
        services.AddTransient<IDecisionService, DecisionService>();
    }
}

using FluentResults;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using TradingApp.Core.EventBus;
using TradingApp.Core.Extensions;
using TradingApp.Evaluator;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes.Dto;
using TradingApp.Module.Quotes.Application.Features.GetCypherB;
using TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;
using TradingApp.Module.Quotes.Application.Features.TickerMetadata;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.CipherB;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.WaveTrend;
using TradingApp.Module.Quotes.Application.Services;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Persistance.Extensions;
using TradingApp.StooqProvider.Setup;
using TradingApp.TingoProvider.Setup;

namespace TradingApp.Module.Quotes.Configuration;

[ExcludeFromCodeCoverage]
public static class QuotesConfigExtension
{
    public static void AddQuotesServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddTransient<IEventBus, EventBus>();
        services.AddDomainEventBus();
        services.AddIntegrationEventBus(configuration);
        services.AddScoped<
            IRequestHandler<
                GetCombinedQuotesCommand,
                IResult<GetCombinedQuotesResponseDto>
            >,
            GetCombinedQuotesCommandHandler
        >();
        services.AddScoped<
            IRequestHandler<GetCypherBCommand, IResult<GetCypherBResponseDto>>,
            GetCypherBCommandHandler
        >();
        services.AddScoped<
            IRequestHandler<EvaluateSRsiCommand, IResultBase>,
            EvaluateSRsiCommandHandler
        >();
        services.AddScoped<
            IRequestHandler<GetTickerMetadataQuery, IResult<CryptocurrencyMetadata[]>>,
            GetTickerMetadataQueryHandler
        >();

        services.AddTransient<IEvaluator, CustomEvaluator>();
        services.AddTransient<ITradingAdapter, TradingAdapter>();
        services.AddTransient<ITradingProviderFactory, TradingProviderFactory>();
        services.AddStooqProvider(configuration);
        services.AddTingoProvider(configuration);
        services.AddMongoDbService(configuration);
        services.AddTransient<ICypherBDecisionService, CypherBDecisionService>();
        services.AddTransient<ISrsiDecisionService, SrsiDecisionService>();

        services.AddTransient<ISrsiStrategyFactory, SrsiStrategyFactory>();
        services.AddTransient<IWaveTrendStrategyFactory, WaveTrendStrategyFactory>();
        services.AddTransient<ICipherBStrategy, CipherBStrategy>();
    }
}

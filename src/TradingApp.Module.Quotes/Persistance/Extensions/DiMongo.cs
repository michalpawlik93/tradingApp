﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Persistance.Configuration;
using TradingApp.Module.Quotes.Persistance.Mappers;
using TradingApp.Module.Quotes.Persistance.Models;
using TradingApp.Module.Quotes.Persistance.Services;

namespace TradingApp.Module.Quotes.Persistance.Extensions;
public static class DiMongo
{
    public static void AddMongoDbService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDBSettings>(configuration.GetSection(MongoDBSettings.ConfigSectionName));
        services.AddMongoDatabase(configuration);
        services.AddSingleton<IEntityDataService<Decision>, MongoDataService<Decision, DecisionDao>>();
        services.AddSingleton<IMongoDbMapper<Decision, DecisionDao>, DecisionMapper>();
    }

    private static void AddMongoDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoDBSettings = configuration.GetSection(MongoDBSettings.ConfigSectionName).Get<MongoDBSettings>();
        ArgumentNullException.ThrowIfNull(mongoDBSettings);
        var client = new MongoClient(mongoDBSettings.ConnectionURI);
        var database = client.GetDatabase(mongoDBSettings.DatabaseName);
        services.AddSingleton(database);
    }
}

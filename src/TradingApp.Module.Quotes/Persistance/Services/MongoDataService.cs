﻿using FluentResults;
using MongoDB.Driver;
using Serilog;
using System.Reflection;
using TradingApp.Core.Attributes;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Persistance.Mappers;
using TradingApp.Module.Quotes.Persistance.Models;

namespace TradingApp.Module.Quotes.Persistance.Services;

public class MongoDataService<TDomain, TDao> : IEntityDataService<TDomain>
    where TDomain : class
    where TDao : BaseDao, new()
{
    private readonly IMongoCollection<TDao> _collection;
    private readonly IMongoDbMapper<TDomain, TDao> _mapper;

    public MongoDataService(IMongoDatabase mongoDatabase, IMongoDbMapper<TDomain, TDao> mapper)
    {
        ArgumentNullException.ThrowIfNull(mongoDatabase);
        _collection = mongoDatabase.GetCollection<TDao>(GetCollectionName(typeof(TDao)));
        ArgumentNullException.ThrowIfNull(mapper);
        _mapper = mapper;
    }

    public async Task<Result> Add(TDomain entity, CancellationToken cancellationToken)
    {
        try
        {
            var dao = _mapper.ToDao(entity);
            if (dao.IsFailed)
            {
                return dao.ToResult();
            }
            await _collection.InsertOneAsync(dao.Value, new InsertOneOptions(), cancellationToken);
            return Result.Ok();
        }
        catch (Exception)
        {
            Log.Logger.Error(
                "Error occured in {serviceName}.",
                nameof(MongoDataService<TDomain, TDao>)
            );
            throw;
        }
    }

    public async Task<Result<TDomain>> Get(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var dao = await _collection
                .Find(d => d.Id == id)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (dao == null)
            {
                return Result.Fail($"{nameof(TDomain)} not found in database");
            }
            var domain = _mapper.ToDomain(dao);
            return domain.IsFailed ? domain.ToResult() : Result.Ok(domain.Value);
        }
        catch (Exception)
        {
            Log.Logger.Error(
                "Error occured in {serviceName}.",
                nameof(MongoDataService<TDomain, TDao>)
            );
            throw;
        }
    }

    private static string GetCollectionName(ICustomAttributeProvider documentType)
    {
        var attr = documentType.GetCustomAttributes(typeof(DataCollectionAttribute), true);
        return (attr.FirstOrDefault() as DataCollectionAttribute)?.CollectionName;
    }
}

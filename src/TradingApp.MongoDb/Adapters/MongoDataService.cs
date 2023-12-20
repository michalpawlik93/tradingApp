using FluentResults;
using MongoDB.Driver;
using Serilog;
using TradingApp.Core.Attributes;
using TradingApp.Module.Quotes.Ports;
using TradingApp.MongoDb.Mappers;
using TradingApp.MongoDb.Models;

namespace TradingApp.MongoDb.Adapters;

public class MongoDataService<TDomain, TDao> : IEntityDataService<TDomain> where TDomain : class where TDao : BaseDao, new()
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
            await _collection.InsertOneAsync(dao, new InsertOneOptions(), cancellationToken);
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
            var dao = await _collection.Find(d => d.Id == id).FirstOrDefaultAsync();
            if (dao == null)
            {
                return Result.Fail($"{nameof(TDomain)} not found in database");
            }
            var domain = _mapper.ToDomain(dao);
            return Result.Ok(domain);
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

    private static protected string GetCollectionName(Type documentType)
    {
        var attr = documentType.GetCustomAttributes(
                typeof(DataCollectionAttribute),
                true);
        if (attr != null)
        {
            var a = attr.FirstOrDefault();
            return ((DataCollectionAttribute)a).CollectionName;
        }

        return ((DataCollectionAttribute)documentType.GetCustomAttributes(
                typeof(DataCollectionAttribute),
                true)
            .FirstOrDefault()).CollectionName;
    }
}

using FluentResults;

namespace TradingApp.Module.Quotes.Persistance.Mappers;

public interface IMongoDbMapper<TDomain, TDao> where TDomain : class where TDao : class, new()
{
    public Result<TDomain> ToDomain(TDao dao);
    public Result<TDao> ToDao(TDomain domainModel);
}

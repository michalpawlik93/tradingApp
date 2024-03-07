namespace TradingApp.Module.Quotes.Persistance.Mappers;

public interface IMongoDbMapper<TDomain, TDao> where TDomain : class where TDao : class, new()
{
    public TDomain ToDomain(TDao dao);
    public TDao ToDao(TDomain domainModel);
}

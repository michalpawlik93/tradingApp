using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.StooqProvider;
using TradingApp.TingoProvider;

namespace TradingApp.Module.Quotes.Application.Services;

public interface ITradingProviderFactory
{
    ITradingProvider CreateProvider(string type);
};

public class TradingProviderFactory : ITradingProviderFactory
{
    private readonly ITingoProvider _tingoProvider;
    private readonly IStooqProvider _stooqProvider;

    public TradingProviderFactory(IStooqProvider stooqProvider, ITingoProvider tingoProvider)
    {
        _tingoProvider = tingoProvider;
        _stooqProvider = stooqProvider;
    }

    public ITradingProvider CreateProvider(string type) => type switch
    {
        nameof(TingoProvider) => _tingoProvider,
        nameof(StooqProvider) => _stooqProvider,
        _ => throw new NotImplementedException()
    };
}



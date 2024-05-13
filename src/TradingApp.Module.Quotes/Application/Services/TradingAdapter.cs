using FluentResults;
using TradingApp.Module.Quotes.Application.Utils;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;

namespace TradingApp.Module.Quotes.Application.Services;

public class TradingAdapter : ITradingAdapter
{
    private readonly ITradingProviderFactory _tradingProviderFactory;
    private ITradingProvider _tradingProvider;

    private const string DefaultProvider = nameof(StooqProvider);

    public TradingAdapter(ITradingProviderFactory tradingProviderFactory)
    {
        ArgumentNullException.ThrowIfNull(tradingProviderFactory);
        _tradingProviderFactory = tradingProviderFactory;
        _tradingProvider = _tradingProviderFactory.CreateProvider(DefaultProvider);
    }

    public void SetProvider(string providerName)
    {
        if (providerName != DefaultProvider)
        {
            _tradingProvider = _tradingProviderFactory.CreateProvider(providerName);
        }
    }

    public async Task<Result<IEnumerable<Quote>>> GetQuotes(
        TimeFrame timeFrame,
        Asset asset,
        PostProcessing postProcessing,
        CancellationToken cancellationToken
    )
    {
        var result = await _tradingProvider.GetQuotes(timeFrame, asset, cancellationToken);
        if (result.IsSuccess && postProcessing.FilterByTimeFrame)
        {
            return result.Value.FilterByTimeFrame(timeFrame).ToResult();
        }
        return result.ToResult<IEnumerable<Quote>>();
    }

    public Task<Result<CryptocurrencyMetadata[]>> GetTickerMetadata(
        string ticker,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}

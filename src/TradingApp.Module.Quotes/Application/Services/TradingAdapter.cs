using FluentResults;
using TradingApp.Module.Quotes.Application.Utils;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;

namespace TradingApp.Module.Quotes.Application.Services;

public class TradingAdapter : ITradingAdapter
{
    private readonly ITradingProviderFactory _tradingProviderFactory;
    public TradingAdapter(ITradingProviderFactory tradingProviderFactory)
    {
        ArgumentNullException.ThrowIfNull(tradingProviderFactory);
        _tradingProviderFactory = tradingProviderFactory;
    }

    public async Task<Result<IEnumerable<Quote>>> GetQuotes(TimeFrame timeFrame, Asset asset, PostProcessing postProcessing, CancellationToken cancellationToken)
    {
        var provider = _tradingProviderFactory.CreateProvider(nameof(TingoProvider));
        var result = await provider.GetQuotes(timeFrame, asset, cancellationToken);
        if (
            result.IsSuccess
            && postProcessing != null
            && postProcessing.FilterByTimeFrame
        ) //can refactor to action pipeline
        {
            return result.Value.FilterByTimeFrame(timeFrame).ToResult();
        }
        return result.ToResult<IEnumerable<Quote>>();
    }

    public Task<Result<CryptocurrencyMetadata[]>> GetTickerMetadata(string ticker, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

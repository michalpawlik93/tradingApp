using FluentResults;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Contract.Ports;

public interface ITradingAdapter
{
    public void SetProvider(string providerName);
    Task<Result<IEnumerable<Quote>>> GetQuotes(TimeFrame timeFrame, Asset asset, PostProcessing postProcessing, CancellationToken cancellationToken);
    Task<Result<CryptocurrencyMetadata[]>> GetTickerMetadata(string ticker, CancellationToken cancellationToken);
}


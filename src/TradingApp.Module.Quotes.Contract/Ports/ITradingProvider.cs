using FluentResults;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Contract.Ports;
public interface ITradingProvider
{
    Task<Result<IEnumerable<Quote>>> GetQuotes(TimeFrame timeFrame, Asset asset, CancellationToken cancellationToken);
    Task<Result<CryptocurrencyMetadata[]>> GetTickerMetadata(Asset asset, CancellationToken cancellationToken);
}

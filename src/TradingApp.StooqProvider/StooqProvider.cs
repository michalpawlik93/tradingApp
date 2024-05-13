using FluentResults;
using System.Diagnostics.CodeAnalysis;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.StooqProvider.Services;

namespace TradingApp.StooqProvider;

public interface IStooqProvider : ITradingProvider;

[ExcludeFromCodeCoverage]
public sealed class StooqProvider : IStooqProvider
{
    private readonly IFileService _fileService;

    public StooqProvider(IFileService fileService)
    {
        ArgumentNullException.ThrowIfNull(fileService);
        _fileService = fileService;
    }

    public async Task<Result<IEnumerable<Quote>>> GetQuotes(TimeFrame timeFrame, Asset asset, CancellationToken cancellationToken)
        => await _fileService.ReadHistoryQuotaFile(timeFrame, asset);

    public Task<Result<CryptocurrencyMetadata[]>> GetTickerMetadata(Asset ticker, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

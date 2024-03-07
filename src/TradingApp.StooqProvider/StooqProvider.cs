using FluentResults;
using System.Diagnostics.CodeAnalysis;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.StooqProvider.Abstraction;
using TradingApp.StooqProvider.Services;

namespace TradingApp.StooqProvider;

[ExcludeFromCodeCoverage]
public sealed class StooqProvider : IStooqProvider
{
    private readonly IFileService _fileService;

    public StooqProvider(IFileService fileService)
    {
        ArgumentNullException.ThrowIfNull(fileService);
        _fileService = fileService;
    }

    public async Task<Result<ICollection<Quote>>> GetQuotesAsync(TimeFrame timeFrame, Asset asset) =>
        await _fileService.ReadHistoryQuotaFile(timeFrame, asset);
}

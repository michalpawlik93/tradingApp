using FluentResults;
using Microsoft.Extensions.Logging;
using TradingApp.TradingAdapter.Enums;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.TradingAdapter.Interfaces;

public abstract class TradingAdapterAbstract
{
    private readonly ILogger<TradingAdapterAbstract> _logger;

    protected TradingAdapterAbstract(ILogger<TradingAdapterAbstract> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Result<AuthorizeResponse>> Authorize(AuthorizeRequest request)
    {
        _logger.LogInformation("BaseClass");
        return await AuthorizeAsync(request);
    }


    public async Task<Result> Logout()
    {
        _logger.LogInformation("BaseClass");
        return await LogoutAsync();
    }

    public async Task<Result<IEnumerable<Quote>>> GetQuotes(HistoryType type = HistoryType.Daily)
    {
        _logger.LogInformation("BaseClass");
        return await GetQuotes(type);
    }

    protected abstract Task<Result<IEnumerable<Quote>>> GetQuotesAsync(HistoryType type);
    protected abstract Task<Result> SaveQuotesAsync(HistoryType type);
    protected abstract Task<Result<AuthorizeResponse>> AuthorizeAsync(AuthorizeRequest request);
    protected abstract Task<Result> LogoutAsync();
}
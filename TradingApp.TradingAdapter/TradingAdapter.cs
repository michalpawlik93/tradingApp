using FluentResults;
using Microsoft.Extensions.Logging;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.TradingAdapter;

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

    protected abstract Task<Result<AuthorizeResponse>> AuthorizeAsync(AuthorizeRequest request);
    protected abstract Task<Result> LogoutAsync();
}

public interface ITradingAdapter
{
    Task<Result<AuthorizeResponse>> Authorize(AuthorizeRequest request);
    Task<Result> Logout();
}
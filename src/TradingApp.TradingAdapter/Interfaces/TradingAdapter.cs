using FluentResults;
using TradingApp.TradingAdapter.Enums;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.TradingAdapter.Interfaces;

/// <summary>
/// This class is used as a proxy
/// </summary>
public abstract class TradingAdapterAbstract
{
    protected TradingAdapterAbstract() { }

    public async Task<Result<AuthorizeResponse>> Authorize(AuthorizeRequest request) => await AuthorizeAsync(request);

    public async Task<Result> Logout() => await LogoutAsync();

    public async Task<Result<IEnumerable<Quote>>> GetQuotes(HistoryType type = HistoryType.Daily)
    {
        return await GetQuotesAsync(type);
    }

    public async Task<Result> SaveQuotes(HistoryType type = HistoryType.Daily) => await SaveQuotesAsync(type, true);

    protected abstract Task<Result<IEnumerable<Quote>>> GetQuotesAsync(HistoryType type);
    protected abstract Task<Result> SaveQuotesAsync(HistoryType type, bool overrideFile);
    protected abstract Task<Result<AuthorizeResponse>> AuthorizeAsync(AuthorizeRequest request);
    protected abstract Task<Result> LogoutAsync();
}

public interface ITradingAdapter
{
    Task<Result<AuthorizeResponse>> Authorize(AuthorizeRequest request);
    Task<Result> Logout();
    Task<Result<IEnumerable<Quote>>> GetQuotes(HistoryType type = HistoryType.Daily);
    Task<Result> SaveQuotes(HistoryType type = HistoryType.Daily);
}
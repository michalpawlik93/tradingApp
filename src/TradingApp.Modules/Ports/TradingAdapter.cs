using FluentResults;
using TradingApp.Modules.Application.Models;
using TradingApp.Ports.Utils;

namespace TradingApp.Modules.Ports;

/// <summary>
/// This class is used as a proxy
/// </summary>
public abstract class TradingAdapterAbstract
{
    protected TradingAdapterAbstract() { }

    public async Task<Result<AuthorizeResponse>> Authorize(AuthorizeRequest request) =>
        await AuthorizeAsync(request);

    public async Task<Result> Logout() => await LogoutAsync();

    public async Task<Result<IEnumerable<Quote>>> GetQuotes(GetQuotesRequest request)
    {
        var result = await GetQuotesAsync(request.TimeFrame, request.Asset);
        if (
            result.IsSuccess
            && request.PostProcessing != null
            && request.PostProcessing.filterByTimeFrame
        ) //can refactot to action pipeline
        {
            return result.Value.FilterByTimeFrame(request.TimeFrame).ToResult();
        }
        return result.ToResult<IEnumerable<Quote>>();
    }

    public async Task<Result> SaveQuotes(TimeFrame timeFrame, Asset asset) =>
        await SaveQuotesAsync(timeFrame, asset, true);

    protected abstract Task<Result<ICollection<Quote>>> GetQuotesAsync(
        TimeFrame timeFrame,
        Asset asset
    );
    protected abstract Task<Result> SaveQuotesAsync(
        TimeFrame timeFrame,
        Asset asset,
        bool overrideFile
    );
    protected abstract Task<Result<AuthorizeResponse>> AuthorizeAsync(AuthorizeRequest request);
    protected abstract Task<Result> LogoutAsync();
}

public interface ITradingAdapter
{
    Task<Result<AuthorizeResponse>> Authorize(AuthorizeRequest request);
    Task<Result> Logout();
    Task<Result<IEnumerable<Quote>>> GetQuotes(GetQuotesRequest request);
    Task<Result> SaveQuotes(TimeFrame timeFrame, Asset asset);
}

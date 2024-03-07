using FluentResults;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Application.Utils;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.StooqProvider.Abstraction;

namespace TradingApp.Module.Quotes.Application.Services;

public interface ITradingAdapter
{
    Task<Result<IEnumerable<Quote>>> GetQuotes(GetQuotesRequest request);
}

public class TradingAdapter : ITradingAdapter
{
    private readonly IStooqProvider _stooqProvider;
    public TradingAdapter(IStooqProvider stooqProvider)
    {
        ArgumentNullException.ThrowIfNull(stooqProvider);
        _stooqProvider = stooqProvider;
    }

    public async Task<Result<IEnumerable<Quote>>> GetQuotes(GetQuotesRequest request)
    {
        var result = await _stooqProvider.GetQuotesAsync(request.TimeFrame, request.Asset);
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
}

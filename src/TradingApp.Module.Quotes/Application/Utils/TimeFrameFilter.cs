using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Utils;

public static class TimeFrameFilter
{
    public static IReadOnlyList<Quote> FilterByTimeFrame(
        this IReadOnlyList<Quote> quotes,
        TimeFrame timeFrame
    ) =>
        timeFrame.StartDate.HasValue && timeFrame.EndDate.HasValue
            ? quotes.Where(q => q.Date >= timeFrame.StartDate && q.Date <= timeFrame.EndDate).ToList()
            : [.. quotes];
}

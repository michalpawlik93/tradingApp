using TradingApp.TradingAdapter.Models;

namespace TradingApp.TradingAdapter.Utils;

public static class TimeFrameFilter
{
    public static IEnumerable<DomainQuote> FilterByTimeFrame(
        this IEnumerable<DomainQuote> quotes,
        TimeFrame timeFrame
    )
    {
        return timeFrame.StartDate.HasValue && timeFrame.EndDate.HasValue
            ? quotes.Where(q => q.Date >= timeFrame.StartDate && q.Date <= timeFrame.EndDate)
            : quotes;
    }
}

using TradingApp.Modules.Application.Models;

namespace TradingApp.Ports.Utils;

public static class TimeFrameFilter
{
    public static IEnumerable<Quote> FilterByTimeFrame(
        this IEnumerable<Quote> quotes,
        TimeFrame timeFrame
    )
    {
        return timeFrame.StartDate.HasValue && timeFrame.EndDate.HasValue
            ? quotes.Where(q => q.Date >= timeFrame.StartDate && q.Date <= timeFrame.EndDate)
            : quotes;
    }
}

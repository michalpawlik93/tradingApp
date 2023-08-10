using TradingApp.Core.Utilities;
using TradingApp.Modules.Quotes.Application.Models;
using TradingApp.TradingAdapter.Enums;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Modules.Quotes.Application.Mappers;

public static class TimeFrameDtoMapper
{
    public static TimeFrame ToDomainModel(TimeFrameDto dto)
    {
        return new TimeFrame(
            Enum.TryParse<Granularity>(dto.Granularity, out var granularityParsed)
                ? granularityParsed
                : Granularity.Hourly,
            DateTimeUtils.ParseIso8601DateString(dto.StartDate),
            DateTimeUtils.ParseIso8601DateString(dto.EndDate)
        );
    }
}

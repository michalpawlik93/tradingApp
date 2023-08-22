using TradingApp.Core.Utilities;
using TradingApp.Modules.Application.Dtos;
using TradingApp.Modules.Application.Models;
using TradingApp.Modules.Domain.Enums;

namespace TradingApp.Modules.Application.Mappers;

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

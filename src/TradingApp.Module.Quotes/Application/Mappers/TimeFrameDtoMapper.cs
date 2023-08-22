using TradingApp.Core.Utilities;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Domain.Enums;

namespace TradingApp.Module.Quotes.Application.Mappers;

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

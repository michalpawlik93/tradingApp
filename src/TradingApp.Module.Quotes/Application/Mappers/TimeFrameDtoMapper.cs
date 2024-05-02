using TradingApp.Core.Utilities;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Mappers;

public static class TimeFrameDtoMapper
{
    public static TimeFrame ToDomainModel(TimeFrameDto dto)
    {
        return new TimeFrame(
            Enum.TryParse<Granularity>(dto.Granularity, out var granularityParsed)
                ? granularityParsed
                : Granularity.Hourly,
            DateTimeUtils.ConvertIso8601_1DateStringToDateTime(dto.StartDate),
            DateTimeUtils.ConvertIso8601_1DateStringToDateTime(dto.EndDate)
        );
    }
}

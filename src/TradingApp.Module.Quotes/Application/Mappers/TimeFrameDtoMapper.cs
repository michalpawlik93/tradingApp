using FluentResults;
using TradingApp.Core.Utilities;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Mappers;

public static class TimeFrameDtoMapper
{
    public static Result<TimeFrame> ToDomainModel(TimeFrameDto dto)
    {
        var startDateResult = DateTimeUtils.ConvertIso8601_1DateStringToDateTime(dto.StartDate);
        if (startDateResult.IsFailed)
        {
            return startDateResult.ToResult();
        }

        var endDateResult = DateTimeUtils.ConvertIso8601_1DateStringToDateTime(dto.EndDate);
        if (endDateResult.IsFailed)
        {
            return endDateResult.ToResult();
        }

        var granularity = Enum.TryParse<Granularity>(dto.Granularity, out var granularityParsed)
            ? granularityParsed
            : Granularity.Hourly;

        return Result.Ok(new TimeFrame(granularity, startDateResult.Value, endDateResult.Value));
    }
}

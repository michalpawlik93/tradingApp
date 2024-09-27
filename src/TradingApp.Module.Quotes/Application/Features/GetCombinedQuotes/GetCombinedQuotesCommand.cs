using FluentResults;
using MediatR;
using System.Collections.Immutable;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes.Dto;
using TradingApp.Module.Quotes.Application.Mappers;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes;

public record Indicators(
    TechnicalIndicator TechnicalIndicator,
    IImmutableSet<SideIndicator> SideIndicators
);

public record SettingsRequest(
    SrsiSettings? SrsiSettings = null,
    RsiSettings? RsiSettings = null,
    MfiSettings? MfiSettings = null,
    WaveTrendSettings? WaveTrendSettings = null
);

public record GetCombinedQuotesCommand(
    ImmutableArray<Indicators> Indicators,
    TimeFrame TimeFrame,
    Asset Asset,
    SettingsRequest SettingsRequest
) : IRequest<IResult<GetCombinedQuotesResponseDto>>;

public static class GetCombinedQuotesCommandExtensions
{
    public static Result<GetCombinedQuotesCommand> CreateCommand(this GetQuotesDtoRequest request)
    {
        var timeFrameResult = TimeFrameDtoMapper.ToDomainModel(request.TimeFrame);
        if (timeFrameResult.IsFailed)
        {
            return timeFrameResult.ToResult();
        }

        return Result.Ok(
            new GetCombinedQuotesCommand(
                IndicatorsMapper.ToDomainModel(request.Indicators),
                timeFrameResult.Value,
                AssetDtoMapper.ToDomainModel(request.Asset),
                SettingsMapper.ToDomainModel(request.Settings)
            )
        );
    }
}

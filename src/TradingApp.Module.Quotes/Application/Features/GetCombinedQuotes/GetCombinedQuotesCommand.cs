using FluentResults;
using MediatR;
using System.Collections.Immutable;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes.Dto;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy;
using TradingApp.Module.Quotes.Application.Mappers;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes;

public record GetCombinedQuotesCommand(
    IImmutableSet<TechnicalIndicator> TechnicalIndicators,
    TimeFrame TimeFrame,
    Asset Asset,
    TradingStrategy TradingStrategy,
    SrsiSettings? SrsiSettings = null
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

        var technicalIndicators = TechnicalIndicatorMapper.ToDomainModel(
            request.TechnicalIndicators
        );
        var asset = AssetDtoMapper.ToDomainModel(request.Asset);
        var tradingStrategy = TradingStrategyMapper.Map(request.TradingStrategy);
        var srsiSettings = SRsiSettingsDtoMapper.ToDomainModel(request.SrsiSettings);

        var command = new GetCombinedQuotesCommand(
            technicalIndicators,
            timeFrameResult.Value,
            asset,
            tradingStrategy,
            srsiSettings
        );

        return Result.Ok(command);
    }
}

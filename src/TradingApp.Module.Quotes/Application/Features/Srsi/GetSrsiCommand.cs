using FluentResults;
using MediatR;
using TradingApp.Module.Quotes.Application.Features.Srsi.Dto;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy;
using TradingApp.Module.Quotes.Application.Mappers;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Features.Srsi;

public record GetSrsiCommand(
    TimeFrame TimeFrame,
    Asset Asset,
    TradingStrategy TradingStrategy,
    SRsiSettings SRsiSettings
) : IRequest<IResult<GetSrsiResponseDto>>;

public static class GetSrsiCommandHandlerExtensions
{
    public static GetSrsiCommand CreateCommand(this GetSrsiRequestDto request) =>
        new(
            TimeFrameDtoMapper.ToDomainModel(request.TimeFrame),
            AssetDtoMapper.ToDomainModel(request.Asset),
            TradingStrategyMapper.Map(request.TradingStrategy),
            SRsiSettingsDtoMapper.ToDomainModel(request.SRsiSettings)
        );
}

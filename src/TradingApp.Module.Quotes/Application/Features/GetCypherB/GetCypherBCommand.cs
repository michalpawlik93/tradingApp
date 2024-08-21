using FluentResults;
using MediatR;
using TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy;
using TradingApp.Module.Quotes.Application.Mappers;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Constants;

namespace TradingApp.Module.Quotes.Application.Features.GetCypherB;

public record GetCypherBCommand(
    TimeFrame TimeFrame,
    Asset Asset,
    WaveTrendSettings WaveTrendSettings,
    SRsiSettings SRsiSettings,
    MfiSettings MfiSettings,
    TradingStrategy TradingStrategy
) : IRequest<IResult<GetCypherBResponseDto>>;

public static class GetCypherBCommandExtensions
{
    public static GetCypherBCommand CreateCommand(this GetCypherBDto request) =>
        new(
            TimeFrameDtoMapper.ToDomainModel(request.TimeFrame),
            AssetDtoMapper.ToDomainModel(request.Asset),
            WaveTrendSettingsDtoMapper.ToDomainModel(request.WaveTrendSettings),
            SRsiSettingsDtoMapper.ToDomainModel(request.SRsiSettings),
            new MfiSettings(request.MfiSettings.ChannelLength, MfiSettingsConst.ScaleFactor),
            TradingStrategyMapper.Map(request.TradingStrategy)
        );
}

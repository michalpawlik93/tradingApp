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
    SrsiSettings? SrsiSettings,
    MfiSettings MfiSettings,
    TradingStrategy TradingStrategy
) : IRequest<IResult<GetCypherBResponseDto>>;

public static class GetCypherBCommandExtensions
{
    public static Result<GetCypherBCommand> CreateCommand(this GetCypherBDto request)
    {
        var timeFrameResult = TimeFrameDtoMapper.ToDomainModel(request.TimeFrame);
        if (timeFrameResult.IsFailed)
        {
            return timeFrameResult.ToResult();
        }

        var asset = AssetDtoMapper.ToDomainModel(request.Asset);
        var waveTrendSettings = WaveTrendSettingsDtoMapper.ToDomainModel(request.WaveTrendSettings);
        var srsiSettings = SRsiSettingsDtoMapper.ToDomainModel(request.SrsiSettings);
        var mfiSettings = new MfiSettings(
            request.MfiSettings.ChannelLength,
            MfiSettingsConst.ScaleFactor
        );
        var tradingStrategy = TradingStrategyMapper.Map(request.TradingStrategy);

        var command = new GetCypherBCommand(
            timeFrameResult.Value,
            asset,
            waveTrendSettings,
            srsiSettings,
            mfiSettings,
            tradingStrategy
        );

        return Result.Ok(command);
    }
}

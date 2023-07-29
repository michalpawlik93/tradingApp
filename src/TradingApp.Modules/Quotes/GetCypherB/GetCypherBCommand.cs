using MediatR;
using TradingApp.Modules.Mappers;
using TradingApp.Modules.Models;
using TradingApp.Modules.Quotes.GetCypherB.Dto;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Modules.Quotes.GetCypherB;

public record GetCypherBCommand(TimeFrame TimeFrame, Asset Asset, WaveTrendSettings WaveTrendSettings, SRsiSettings SRsiSettings)
    : IRequest<ServiceResponse<GetCypherBResponseDto>>;

public static class GetCypherBCommandExtensions
{
    public static GetCypherBCommand CreateCommand(this GetCypherBDto request)
    {
        return new GetCypherBCommand(
            TimeFrameDtoMapper.ToDomainModel(request.TimeFrame),
            AssetDtoMapper.ToDomainModel(request.Asset),
            WaveTrendSettingsDtoMapper.ToDomainModel(request.WaveTrendSettings),
            SRsiSettingsDtoMapper.ToDomainModel(request.SRsiSettings)
        );
    }
}


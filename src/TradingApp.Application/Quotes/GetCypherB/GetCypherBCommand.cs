using MediatR;
using TradingApp.Application.Mappers;
using TradingApp.Application.Models;
using TradingApp.Application.Quotes.GetCypherB.Dto;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Application.Quotes.GetCypherB;

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


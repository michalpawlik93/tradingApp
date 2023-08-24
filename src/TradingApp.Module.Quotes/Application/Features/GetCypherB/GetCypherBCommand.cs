using MediatR;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;
using TradingApp.Module.Quotes.Application.Mappers;
using TradingApp.Module.Quotes.Application.Models;

namespace TradingApp.Module.Quotes.Application.Features.GetCypherB;

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


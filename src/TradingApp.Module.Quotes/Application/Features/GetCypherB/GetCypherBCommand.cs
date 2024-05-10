using FluentResults;
using MediatR;
using TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;
using TradingApp.Module.Quotes.Application.Mappers;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Features.GetCypherB;

public record GetCypherBCommand(TimeFrame TimeFrame, Asset Asset, WaveTrendSettings WaveTrendSettings, SRsiSettings SRsiSettings)
    : IRequest<IResult<GetCypherBResponseDto>>;

public static class GetCypherBCommandExtensions
{
    public static GetCypherBCommand CreateCommand(this GetCypherBDto request) =>
        new(
            TimeFrameDtoMapper.ToDomainModel(request.TimeFrame),
            AssetDtoMapper.ToDomainModel(request.Asset),
            WaveTrendSettingsDtoMapper.ToDomainModel(request.WaveTrendSettings),
            SRsiSettingsDtoMapper.ToDomainModel(request.SRsiSettings)
        );
}


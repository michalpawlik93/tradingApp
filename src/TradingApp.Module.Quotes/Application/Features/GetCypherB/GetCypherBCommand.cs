using FluentResults;
using MediatR;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes;
using TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;
using TradingApp.Module.Quotes.Application.Mappers;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Features.GetCypherB;

public record GetCypherBCommand(TimeFrame TimeFrame, Asset Asset, SettingsRequest SettingsRequest)
    : IRequest<IResult<GetCypherBResponseDto>>;

public static class GetCypherBCommandExtensions
{
    public static Result<GetCypherBCommand> CreateCommand(this GetCypherBDto request)
    {
        var timeFrameResult = TimeFrameDtoMapper.ToDomainModel(request.TimeFrame);
        if (timeFrameResult.IsFailed)
        {
            return timeFrameResult.ToResult();
        }

        var command = new GetCypherBCommand(
            timeFrameResult.Value,
            AssetDtoMapper.ToDomainModel(request.Asset),
            SettingsMapper.ToDomainModel(request.Settings)
        );

        return Result.Ok(command);
    }
}

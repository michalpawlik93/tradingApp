using MediatR;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes.Dto;
using TradingApp.Module.Quotes.Application.Mappers;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes;

public record GetCombinedQuotesCommand(TimeFrame TimeFrame, Asset Asset)
    : IRequest<ServiceResponse<GetCombinedQuotesResponseDto>>;

public static class GetCombinedQuotesCommandExtensions
{
    public static GetCombinedQuotesCommand CreateCommandRequest(
        string granularity,
        string assetType,
        string assetName,
        string startDate,
        string endDate
    ) =>
        new(
            TimeFrameDtoMapper.ToDomainModel(
                new TimeFrameDto
                {
                    Granularity = granularity,
                    StartDate = startDate,
                    EndDate = endDate
                }
            ),
            AssetDtoMapper.ToDomainModel(new AssetDto { Name = assetName, Type = assetType })
        );
}

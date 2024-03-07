using MediatR;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Features.GetStooqCombinedQuotes.Dto;
using TradingApp.Module.Quotes.Application.Mappers;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Features.GetStooqCombinedQuotes;

public record GetStooqCombinedQuotesCommand(TimeFrame TimeFrame, Asset Asset)
    : IRequest<ServiceResponse<GetStooqCombinedQuotesResponseDto>>;

public static class GetStooqCombinedQuotesCommandExtensions
{
    public static GetStooqCombinedQuotesCommand CreateCommandRequest(
        string granularity,
        string assetType,
        string assetName,
        string startDate,
        string endDate
    )
    {
        return new GetStooqCombinedQuotesCommand(
            TimeFrameDtoMapper.ToDomainModel(
                new TimeFrameDto()
                {
                    Granularity = granularity,
                    StartDate = startDate,
                    EndDate = endDate
                }
            ),
            AssetDtoMapper.ToDomainModel(new AssetDto() { Name = assetName, Type = assetType })
        );
    }
}

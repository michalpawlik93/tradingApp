using MediatR;
using TradingApp.Application.Mappers;
using TradingApp.Application.Models;
using TradingApp.Application.Quotes.GetStooqCombinedQuotes.Dto;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Application.Quotes.GetStooqQuotes;

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

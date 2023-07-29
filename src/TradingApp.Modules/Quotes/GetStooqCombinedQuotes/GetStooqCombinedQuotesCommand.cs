using MediatR;
using TradingApp.Core.Models;
using TradingApp.Modules.Quotes.GetStooqCombinedQuotes.Dto;
using TradingApp.Modules.Quotes.Mappers;
using TradingApp.Modules.Quotes.Models;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Modules.Quotes.GetStooqQuotes;

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

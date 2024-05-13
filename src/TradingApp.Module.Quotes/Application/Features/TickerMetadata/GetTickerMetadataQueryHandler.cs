using FluentResults;
using MediatR;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Mappers;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.TingoProvider;

namespace TradingApp.Module.Quotes.Application.Features.TickerMetadata;

public record GetTickerMetadataQuery(Asset Asset) : IRequest<IResult<CryptocurrencyMetadata[]>>;
public class GetTickerMetadataQueryHandler(ITingoProvider tingoProvider) : IRequestHandler<GetTickerMetadataQuery, IResult<CryptocurrencyMetadata[]>>
{
    public async Task<IResult<CryptocurrencyMetadata[]>> Handle(GetTickerMetadataQuery request, CancellationToken cancellationToken) =>
        await tingoProvider.GetTickerMetadata(request.Asset, cancellationToken);
}

public static class GetTickerMetadataQueryExtensions
{
    public static GetTickerMetadataQuery CreateCommandRequest(
        AssetAsParamsDto request
    ) =>
        new(
            AssetDtoMapper.ToDomainModel(request)
        );
}



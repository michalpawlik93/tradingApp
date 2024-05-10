using FluentResults;
using MediatR;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.TingoProvider;

namespace TradingApp.Module.Quotes.Application.Features.TickerMetadata;

public record GetTickerMetadataQuery(string Ticker) : IRequest<IResult<CryptocurrencyMetadata[]>>;
public class GetTickerMetadataQueryHandler(ITingoProvider tingoProvider) : IRequestHandler<GetTickerMetadataQuery, IResult<CryptocurrencyMetadata[]>>
{
    public async Task<IResult<CryptocurrencyMetadata[]>> Handle(GetTickerMetadataQuery request, CancellationToken cancellationToken) =>
        await tingoProvider.GetTickerMetadata(request.Ticker, cancellationToken);
}


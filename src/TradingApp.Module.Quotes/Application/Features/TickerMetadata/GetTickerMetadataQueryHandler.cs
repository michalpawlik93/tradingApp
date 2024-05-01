using MediatR;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.TingoProvider;

namespace TradingApp.Module.Quotes.Application.Features.TickerMetadata;

public record GetTickerMetadataQuery(string Ticker) : IRequest<ServiceResponse<CryptocurrencyMetadata[]>>;
public class GetTickerMetadataQueryHandler(ITingoProvider tingoProvider) : IRequestHandler<GetTickerMetadataQuery, ServiceResponse<CryptocurrencyMetadata[]>>
{
    public async Task<ServiceResponse<CryptocurrencyMetadata[]>> Handle(GetTickerMetadataQuery request, CancellationToken cancellationToken)
    {

        var result = await tingoProvider.GetTickerMetadata(request.Ticker, cancellationToken);
        return new ServiceResponse<CryptocurrencyMetadata[]>(result);
    }
}


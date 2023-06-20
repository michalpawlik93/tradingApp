using FluentResults;
using MediatR;
using TradingApp.Application.Models;
using TradingApp.StooqProvider;
using TradingApp.TradingAdapter.Evaluator;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Application.Quotes.GetCypherB;

public class GetCypherBCommandHandler
    : IRequestHandler<GetCypherBCommand, ServiceResponse<GetCypherBResponse>>
{
    private readonly IStooqProvider _provider;
    private readonly ISkenderEvaluator _skenderEvaluator;

    public GetCypherBCommandHandler(IStooqProvider provider, ISkenderEvaluator skenderEvaluator)
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(skenderEvaluator);
        _provider = provider;
        _skenderEvaluator = skenderEvaluator;
    }

    public async Task<ServiceResponse<GetCypherBResponse>> Handle(
        GetCypherBCommand request,
        CancellationToken cancellationToken
    )
    {
        var getQuotesResponse = await _provider.GetQuotes(request.Granularity);
        if (getQuotesResponse.IsFailed)
        {
            return new ServiceResponse<GetCypherBResponse>(getQuotesResponse.ToResult());
        }
        var mfiResults = _skenderEvaluator.GetMFI(getQuotesResponse.Value);
        var momentumWaveResults = _skenderEvaluator.GetMomentumWave(getQuotesResponse.Value);
        var vwapResults = _skenderEvaluator.GetVwap(getQuotesResponse.Value);
        var combinedResults = getQuotesResponse.Value
            .Select(
                (q, i) =>
                    new CypherBQuote(
                        q,
                        mfiResults.ElementAt(i),
                        momentumWaveResults.ElementAt(i),
                        vwapResults.ElementAt(i)
                    )
            )
            .ToList();
        return new ServiceResponse<GetCypherBResponse>(
            Result.Ok(new GetCypherBResponse(combinedResults))
        );
    }
}

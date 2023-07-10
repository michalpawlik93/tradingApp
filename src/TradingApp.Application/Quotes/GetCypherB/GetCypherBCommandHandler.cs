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
    private readonly ICustomEvaluator _evaluator;

    public GetCypherBCommandHandler(IStooqProvider provider, ICustomEvaluator evaluator)
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(evaluator);
        _provider = provider;
        _evaluator = evaluator;
    }

    public async Task<ServiceResponse<GetCypherBResponse>> Handle(
        GetCypherBCommand request,
        CancellationToken cancellationToken
    )
    {
        var getQuotesResponse = await _provider.GetQuotes(new GetQuotesRequest(request.TimeFrame, request.Asset, new PostProcessing(true)));
        if (getQuotesResponse.IsFailed)
        {
            return new ServiceResponse<GetCypherBResponse>(getQuotesResponse.ToResult());
        }
        var mfi = _evaluator.GetMFI(getQuotesResponse.Value);
        var waveTrend = _evaluator.GetWaveTrend(getQuotesResponse.Value);
        var vwap = _evaluator.GetVwap(getQuotesResponse.Value);
        var combinedResults = getQuotesResponse.Value
            .Select(
                (q, i) =>
                    new CypherBQuote(
                        q,
                        waveTrend.ElementAt(i),
                        mfi.ElementAt(i),
                        vwap.ElementAt(i)
                    )
            )
            .ToList();
        return new ServiceResponse<GetCypherBResponse>(
            Result.Ok(new GetCypherBResponse(combinedResults))
        );
    }
}

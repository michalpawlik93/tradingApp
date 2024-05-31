using FluentResults;
using MediatR;
using TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;

namespace TradingApp.Module.Quotes.Application.Features.GetCypherB;

public class GetCypherBCommandHandler
    : IRequestHandler<GetCypherBCommand, IResult<GetCypherBResponseDto>>
{
    private readonly ITradingAdapter _provider;
    private readonly IEvaluator _evaluator;

    public GetCypherBCommandHandler(ITradingAdapter provider, IEvaluator evaluator)
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(evaluator);
        _provider = provider;
        _evaluator = evaluator;
    }

    public async Task<IResult<GetCypherBResponseDto>> Handle(
        GetCypherBCommand request,
        CancellationToken cancellationToken
    )
    {
        var getQuotesResponse = await _provider.GetQuotes(
            request.TimeFrame, request.Asset, new PostProcessing(true), cancellationToken
        );
        if (getQuotesResponse.IsFailed)
        {
            return getQuotesResponse.ToResult<GetCypherBResponseDto>();
        }

        var quotes = getQuotesResponse.Value.ToList();
        var waveTrend = _evaluator.GetWaveTrend(
            quotes,
            request.WaveTrendSettings
        );
        var mfi = _evaluator.GetMfi(
            quotes,
            request.MfiSettings
        );
        var combinedResults = quotes
            .Select(
                (q, i) => new CypherBQuote(q, waveTrend.ElementAt(i), mfi.ElementAt(i))
            )
            .ToList();
        return Result.Ok(new GetCypherBResponseDto(combinedResults));
    }
}

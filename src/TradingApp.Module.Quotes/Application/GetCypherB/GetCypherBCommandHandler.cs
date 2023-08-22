using FluentResults;
using MediatR;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Application.GetCypherB.Dto;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Ports;

namespace TradingApp.Module.Quotes.Application.GetCypherB;

public class GetCypherBCommandHandler
    : IRequestHandler<GetCypherBCommand, ServiceResponse<GetCypherBResponseDto>>
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

    public async Task<ServiceResponse<GetCypherBResponseDto>> Handle(
        GetCypherBCommand request,
        CancellationToken cancellationToken
    )
    {
        var getQuotesResponse = await _provider.GetQuotes(
            new GetQuotesRequest(request.TimeFrame, request.Asset, new PostProcessing(true))
        );
        if (getQuotesResponse.IsFailed)
        {
            return new ServiceResponse<GetCypherBResponseDto>(getQuotesResponse.ToResult());
        }

        var quotes = getQuotesResponse.Value.ToList();
        var waveTrend = _evaluator.GetWaveTrend(
            quotes,
            new WaveTrendSettings(
                request.WaveTrendSettings.Oversold,
                request.WaveTrendSettings.Overbought,
                request.WaveTrendSettings.ChannelLength,
                request.WaveTrendSettings.AverageLength,
                request.WaveTrendSettings.MovingAverageLength
            )
        );
        var vwap = _evaluator.GetVwap(quotes);
        var combinedResults = quotes
            .Select(
                (q, i) => new CypherBQuote(q, waveTrend.ElementAt(i), null, vwap.ElementAt(i).Value)
            )
            .ToList();
        return new ServiceResponse<GetCypherBResponseDto>(
            Result.Ok(new GetCypherBResponseDto(combinedResults))
        );
    }
}

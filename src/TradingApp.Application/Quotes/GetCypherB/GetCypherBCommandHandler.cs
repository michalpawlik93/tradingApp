using FluentResults;
using MediatR;
using TradingApp.Application.Models;
using TradingApp.Application.Quotes.GetCypherB.Dto;
using TradingApp.StooqProvider;
using TradingApp.TradingAdapter.Evaluator;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Application.Quotes.GetCypherB;

public class GetCypherBCommandHandler
    : IRequestHandler<GetCypherBCommand, ServiceResponse<GetCypherBResponseDto>>
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
        var mfi = _evaluator.GetMFI(getQuotesResponse.Value);

        int channellen = 8; // Channel Length
        int averagelen = 6; // Average Length
        int wt1malen = 3; // Moving Average Length
        int channellen2 = 13; // Channel Length
        int averagelen2 = 55; // Average Length
        var waveTrend = _evaluator.GetWaveTrend(
            getQuotesResponse.Value,
            new WaveTrendSettings(
                request.WaveTrendSettings.RsiSettings,
                request.WaveTrendSettings.ChannelLength,
                request.WaveTrendSettings.AverageLength,
                request.WaveTrendSettings.MovingAverageLength
            )
        );
        var vwap = _evaluator.GetVwap(getQuotesResponse.Value.ToList());
        var combinedResults = getQuotesResponse.Value
            .Select(
                (q, i) =>
                    new CypherBQuote(q, waveTrend.ElementAt(i), mfi.ElementAt(i), vwap.ElementAt(i))
            )
            .ToList();
        return new ServiceResponse<GetCypherBResponseDto>(
            Result.Ok(new GetCypherBResponseDto(combinedResults))
        );
    }
}

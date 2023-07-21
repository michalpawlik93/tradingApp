using FluentResults;
using MediatR;
using Serilog;
using TradingApp.Application.Models;
using TradingApp.Application.Quotes.GetStooqCombinedQuotes.Dto;
using TradingApp.StooqProvider;
using TradingApp.TradingAdapter.Constants;
using TradingApp.TradingAdapter.Evaluator;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Application.Quotes.GetStooqQuotes;

public class GetStooqCombinedQuotesCommandHandler
    : IRequestHandler<
        GetStooqCombinedQuotesCommand,
        ServiceResponse<GetStooqCombinedQuotesResponseDto>
    >
{
    private readonly IStooqProvider _provider;
    private readonly ISkenderEvaluator _skenderEvaluator;
    private const int RSIPeriod = 14;

    public GetStooqCombinedQuotesCommandHandler(
        IStooqProvider provider,
        ISkenderEvaluator skenderEvaluator
    )
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(skenderEvaluator);
        _provider = provider;
        _skenderEvaluator = skenderEvaluator;
    }

    public async Task<ServiceResponse<GetStooqCombinedQuotesResponseDto>> Handle(
        GetStooqCombinedQuotesCommand request,
        CancellationToken cancellationToken
    )
    {
        Log.Logger.Information(
            "{handlerName} started.",
            nameof(GetStooqCombinedQuotesCommandHandler)
        );
        var getQuotesResponse = await _provider.GetQuotes(
            new GetQuotesRequest(request.TimeFrame, request.Asset, new PostProcessing(true))
        );
        if (getQuotesResponse.IsFailed)
        {
            return new ServiceResponse<GetStooqCombinedQuotesResponseDto>(
                getQuotesResponse.ToResult()
            );
        }
        var rsiResults = _skenderEvaluator.GetRSI(getQuotesResponse.Value, RSIPeriod);
        var combinedResults = getQuotesResponse.Value
            .Select((q, i) => new CombinedQuote(q, rsiResults.ElementAt(i), null))
            .ToList();
        return new ServiceResponse<GetStooqCombinedQuotesResponseDto>(
            Result.Ok(
                new GetStooqCombinedQuotesResponseDto(
                    combinedResults,
                    new RsiSettings(RsiSettingsConst.Oversold, RsiSettingsConst.Overbought)
                )
            )
        );
    }
}

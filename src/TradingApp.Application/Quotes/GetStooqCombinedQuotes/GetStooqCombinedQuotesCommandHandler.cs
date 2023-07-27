using FluentResults;
using MediatR;
using Serilog;
using TradingApp.Application.Models;
using TradingApp.Application.Quotes.GetStooqCombinedQuotes.Dto;
using TradingApp.StooqProvider;
using TradingApp.TradingAdapter.Constants;
using TradingApp.TradingAdapter.Interfaces;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Application.Quotes.GetStooqQuotes;

public class GetStooqCombinedQuotesCommandHandler
    : IRequestHandler<
        GetStooqCombinedQuotesCommand,
        ServiceResponse<GetStooqCombinedQuotesResponseDto>
    >
{
    private readonly IStooqProvider _provider;
    private readonly IEvaluator _customEvaluator;

    public GetStooqCombinedQuotesCommandHandler(IStooqProvider provider, IEvaluator customEvaluator)
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(customEvaluator);
        _provider = provider;
        _customEvaluator = customEvaluator;
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
        var rsiResults = _customEvaluator.GetRSI(
            getQuotesResponse.Value.ToList(),
            new RsiSettings(
                RsiSettingsConst.Oversold,
                RsiSettingsConst.Overbought,
                true,
                RsiSettingsConst.DefaultPeriod
            )
        );
        var combinedResults = getQuotesResponse.Value
            .Select((q, i) => new CombinedQuote(q, rsiResults.ElementAt(i).Value, null))
            .ToList();
        return new ServiceResponse<GetStooqCombinedQuotesResponseDto>(
            Result.Ok(
                new GetStooqCombinedQuotesResponseDto(
                    combinedResults,
                    new RsiSettings(
                        RsiSettingsConst.Oversold,
                        RsiSettingsConst.Overbought,
                        true,
                        14
                    )
                )
            )
        );
    }
}

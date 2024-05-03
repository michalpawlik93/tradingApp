using FluentResults;
using MediatR;
using Serilog;
using TradingApp.Core.Models;
using TradingApp.Domain.Modules.Constants;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes.Dto;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;

namespace TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes;

public class GetCombinedQuotesCommandHandler
    : IRequestHandler<
        GetCombinedQuotesCommand,
        ServiceResponse<GetCombinedQuotesResponseDto>
    >
{
    private readonly ITradingAdapter _adapter;
    private readonly IEvaluator _customEvaluator;

    public GetCombinedQuotesCommandHandler(ITradingAdapter adapter, IEvaluator customEvaluator)
    {
        ArgumentNullException.ThrowIfNull(adapter);
        ArgumentNullException.ThrowIfNull(customEvaluator);
        _adapter = adapter;
        _customEvaluator = customEvaluator;
    }

    public async Task<ServiceResponse<GetCombinedQuotesResponseDto>> Handle(
        GetCombinedQuotesCommand request,
        CancellationToken cancellationToken
    )
    {
        Log.Logger.Information(
            "{handlerName} started.",
            nameof(GetCombinedQuotesCommandHandler)
        );
        var getQuotesResponse = await _adapter.GetQuotes(
            request.TimeFrame, request.Asset, new PostProcessing(true), cancellationToken
        );
        if (getQuotesResponse.IsFailed)
        {
            return new ServiceResponse<GetCombinedQuotesResponseDto>(
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
        return new ServiceResponse<GetCombinedQuotesResponseDto>(
            Result.Ok(
                new GetCombinedQuotesResponseDto(
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

using FluentResults;
using MediatR;
using Serilog;
using TradingApp.Core.Models;
using TradingApp.Domain.Modules.Constants;
using TradingApp.Module.Quotes.Application.Features.GetStooqCombinedQuotes.Dto;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Application.Services;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;

namespace TradingApp.Module.Quotes.Application.Features.GetStooqCombinedQuotes;

public class GetStooqCombinedQuotesCommandHandler
    : IRequestHandler<
        GetStooqCombinedQuotesCommand,
        ServiceResponse<GetStooqCombinedQuotesResponseDto>
    >
{
    private readonly ITradingAdapter _provider;
    private readonly IEvaluator _customEvaluator;

    public GetStooqCombinedQuotesCommandHandler(ITradingAdapter provider, IEvaluator customEvaluator)
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

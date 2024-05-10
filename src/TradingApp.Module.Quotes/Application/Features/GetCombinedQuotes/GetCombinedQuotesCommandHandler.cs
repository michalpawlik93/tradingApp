using FluentResults;
using MediatR;
using Serilog;
using TradingApp.Domain.Modules.Constants;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes.Dto;
using TradingApp.Module.Quotes.Application.Mappers;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;

namespace TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes;

public class GetCombinedQuotesCommandHandler
    : IRequestHandler<GetCombinedQuotesCommand, IResult<GetCombinedQuotesResponseDto>>
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

    public async Task<IResult<GetCombinedQuotesResponseDto>> Handle(
        GetCombinedQuotesCommand request,
        CancellationToken cancellationToken
    )
    {
        Log.Logger.Information("{handlerName} started.", nameof(GetCombinedQuotesCommandHandler));
        var getQuotesResponse = await _adapter.GetQuotes(
            request.TimeFrame,
            request.Asset,
            new PostProcessing(true),
            cancellationToken
        );
        if (getQuotesResponse.IsFailed)
        {
            return getQuotesResponse.ToResult<GetCombinedQuotesResponseDto>();
        }

        ICollection<RsiResult> rsiResults = null;
        var includeRsi = request.TechnicalIndicators.Contains(TechnicalIndicator.Rsi);
        if (includeRsi)
        {
            rsiResults = _customEvaluator.GetRSI(
                getQuotesResponse.Value.ToList(),
                new RsiSettings(
                    RsiSettingsConst.Oversold,
                    RsiSettingsConst.Overbought,
                    true,
                    RsiSettingsConst.DefaultPeriod
                )
            );
        }

        return Result.Ok(
            GetCombinedQuotesResponseMapper.ToDto(getQuotesResponse.Value, rsiResults, includeRsi)
        );
    }
}

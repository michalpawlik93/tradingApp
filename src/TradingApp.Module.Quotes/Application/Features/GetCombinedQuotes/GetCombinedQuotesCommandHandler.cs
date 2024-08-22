using FluentResults;
using MediatR;
using Serilog;
using TradingApp.Domain.Modules.Constants;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes.Dto;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;
using TradingApp.Module.Quotes.Application.Mappers;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;

namespace TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes;

public class GetCombinedQuotesCommandHandler
    : IRequestHandler<GetCombinedQuotesCommand, IResult<GetCombinedQuotesResponseDto>>
{
    private readonly ITradingAdapter _adapter;
    private readonly IEvaluator _customEvaluator;
    private readonly ISrsiStrategyFactory _srsiStrategyFactory;

    public GetCombinedQuotesCommandHandler(
        ITradingAdapter adapter,
        IEvaluator customEvaluator,
        ISrsiStrategyFactory srsiStrategyFactory
    )
    {
        ArgumentNullException.ThrowIfNull(adapter);
        ArgumentNullException.ThrowIfNull(customEvaluator);
        ArgumentNullException.ThrowIfNull(srsiStrategyFactory);
        _adapter = adapter;
        _customEvaluator = customEvaluator;
        _srsiStrategyFactory = srsiStrategyFactory;
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

        var quotes = getQuotesResponse.Value.ToList();
        var rsiResults = GetRsiResults(request, quotes);

        var srsiResponse = GetSrsiResult(request, quotes);
        if (srsiResponse is { IsFailed: true })
        {
            return srsiResponse.ToResult<GetCombinedQuotesResponseDto>();
        }


        return Result.Ok(
            GetCombinedQuotesResponseMapper.ToDto(getQuotesResponse.Value, rsiResults, srsiResponse?.Value)
        );
    }

    private IReadOnlyList<RsiResult> GetRsiResults(
        GetCombinedQuotesCommand request,
        IReadOnlyList<Quote> quotes
    ) =>
        request.TechnicalIndicators.Contains(TechnicalIndicator.Rsi)
            ? _customEvaluator.GetRsi(
                quotes,
                new RsiSettings(
                    RsiSettingsConst.Oversold,
                    RsiSettingsConst.Overbought,
                    true,
                    RsiSettingsConst.DefaultPeriod
                )
            )
            : [];

    private Result<IReadOnlyList<SrsiSignal>> GetSrsiResult(
        GetCombinedQuotesCommand request,
        IReadOnlyList<Quote> quotes
    ) => request.TechnicalIndicators.Contains(TechnicalIndicator.Srsi)
        ? _srsiStrategyFactory.GetStrategy(
            request.TradingStrategy,
            request.TimeFrame.Granularity
        ).EvaluateSignals(quotes, request.SrsiSettings)
        : null;
}

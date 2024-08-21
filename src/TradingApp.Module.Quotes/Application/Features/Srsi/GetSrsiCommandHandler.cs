using FluentResults;
using MediatR;
using Serilog;
using TradingApp.Module.Quotes.Application.Features.Srsi.Dto;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;

namespace TradingApp.Module.Quotes.Application.Features.Srsi;

public class GetSrsiCommandHandler : IRequestHandler<GetSrsiCommand, IResult<GetSrsiResponseDto>>
{
    private readonly ITradingAdapter _adapter;
    private readonly ISrsiStrategyFactory _srsiStrategyFactory;

    public GetSrsiCommandHandler(ITradingAdapter adapter, ISrsiStrategyFactory srsiStrategyFactory)
    {
        ArgumentNullException.ThrowIfNull(adapter);
        ArgumentNullException.ThrowIfNull(srsiStrategyFactory);
        _adapter = adapter;
        _srsiStrategyFactory = srsiStrategyFactory;
    }

    public async Task<IResult<GetSrsiResponseDto>> Handle(
        GetSrsiCommand request,
        CancellationToken cancellationToken
    )
    {
        Log.Logger.Information("{handlerName} started.", nameof(GetSrsiCommandHandler));
        var getQuotesResponse = await _adapter.GetQuotes(
            request.TimeFrame,
            request.Asset,
            new PostProcessing(true),
            cancellationToken
        );
        if (getQuotesResponse.IsFailed)
        {
            return getQuotesResponse.ToResult<GetSrsiResponseDto>();
        }

        var strategy = _srsiStrategyFactory.GetStrategy(
            request.TradingStrategy,
            request.TimeFrame.Granularity
        );
        var quotes = getQuotesResponse.Value.ToList();
        var srsiResponse = strategy.EvaluateSignals(quotes);
        if (srsiResponse.IsFailed)
        {
            return srsiResponse.ToResult<GetSrsiResponseDto>();
        }

        return Result.Ok(
            new GetSrsiResponseDto(
                quotes.Select((q, i) => new SrsiDto(q, srsiResponse.Value[i])).ToList()
            )
        );
    }
}

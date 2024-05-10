﻿using FluentResults;
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
        return Result.Ok(new GetCypherBResponseDto(combinedResults));
    }
}

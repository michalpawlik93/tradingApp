using FluentResults;
using MediatR;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;

namespace TradingApp.Module.Quotes.Application.Features.GetCypherB;

public class GetCypherBCommandHandler
    : IRequestHandler<GetCypherBCommand, IResult<GetCypherBResponseDto>>
{
    private readonly ITradingAdapter _provider;
    private readonly ICypherBDecisionService _cypherBDecisionService;

    public GetCypherBCommandHandler(
        ITradingAdapter provider,
        ICypherBDecisionService cypherBDecisionService
    )
    {
        ArgumentNullException.ThrowIfNull(provider);
        _provider = provider;
        _cypherBDecisionService = cypherBDecisionService;
    }

    public async Task<IResult<GetCypherBResponseDto>> Handle(
        GetCypherBCommand request,
        CancellationToken cancellationToken
    )
    {
        var getQuotesResponse = await _provider.GetQuotes(
            request.TimeFrame,
            request.Asset,
            new PostProcessing(true),
            cancellationToken
        );
        if (getQuotesResponse.IsFailed)
        {
            return getQuotesResponse.ToResult<GetCypherBResponseDto>();
        }

        var results = _cypherBDecisionService.GetQuotesTradeActions(
            getQuotesResponse.Value.ToList(),
            new CypherBDecisionSettings(
                request.TimeFrame.Granularity,
                request.WaveTrendSettings,
                request.MfiSettings
            )
        );
        if (results.IsFailed)
        {
            return getQuotesResponse.ToResult<GetCypherBResponseDto>();
        }
        return Result.Ok(new GetCypherBResponseDto(results.Value));
    }
}

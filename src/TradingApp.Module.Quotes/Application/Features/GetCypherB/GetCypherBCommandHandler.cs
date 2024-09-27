using FluentResults;
using MediatR;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.CipherB;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;

namespace TradingApp.Module.Quotes.Application.Features.GetCypherB;

public class GetCypherBCommandHandler
    : IRequestHandler<GetCypherBCommand, IResult<GetCypherBResponseDto>>
{
    private readonly ITradingAdapter _provider;
    private readonly ICipherBStrategy _cipherBStrategy;

    public GetCypherBCommandHandler(ITradingAdapter provider, ICipherBStrategy cipherBStrategy)
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(cipherBStrategy);
        _provider = provider;
        _cipherBStrategy = cipherBStrategy;
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

        var quotes = getQuotesResponse.Value.ToList();
        var evaluateSignalsResponse = _cipherBStrategy.EvaluateSignals(
            quotes,
            new CypherBDecisionSettings(
                request.TimeFrame.Granularity,
                request.SettingsRequest.WaveTrendSettings,
                request.SettingsRequest.MfiSettings,
                request.SettingsRequest.SrsiSettings,
                request.Asset.Name
            )
        );

        if (evaluateSignalsResponse.IsFailed)
        {
            return evaluateSignalsResponse.ToResult<GetCypherBResponseDto>();
        }

        var (mfiResults, waveTrendSignals, srsiSignals) = evaluateSignalsResponse.Value;

        var responseDto = new GetCypherBResponseDto(
            quotes
                .Select(
                    (q, i) =>
                        new CypherBQuote(
                            q,
                            waveTrendSignals.ElementAtOrDefault(i),
                            mfiResults.ElementAtOrDefault(i),
                            srsiSignals.ElementAtOrDefault(i)
                        )
                )
                .ToList()
        );

        return Result.Ok(responseDto);
    }
}

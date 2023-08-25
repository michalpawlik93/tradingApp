using FluentResults;
using MediatR;
using TradingApp.Core.EventBus;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Application.Services;
using TradingApp.Module.Quotes.Domain.Constants;
using TradingApp.Module.Quotes.Domain.ValueObjects;
using TradingApp.Module.Quotes.Ports;

namespace TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;

/// <summary>
/// Evaluate decision for last date in quotes
/// </summary>
/// <param name="Quotes"></param>
public record EvaluateSRsiCommand(List<Quote> Quotes) : IRequest<ServiceResponse>;

public class EvaluateSRsiCommandHandler : IRequestHandler<EvaluateSRsiCommand, ServiceResponse>
{
    private readonly IEventBus _eventBus;
    private readonly IEvaluator _evaulator;
    private readonly IDecisionService _decisionService;
    private readonly IDecisionDataService _decisionDataService;

    private static SRsiSettings SrsiSettings = new(true, 12, 3, 3, -60, 60);

    public EvaluateSRsiCommandHandler(
        IEventBus eventBus,
        IEvaluator evaulator,
        IDecisionService decisionService,
        IDecisionDataService decisionDataService
    )
    {
        ArgumentNullException.ThrowIfNull(eventBus);
        ArgumentNullException.ThrowIfNull(evaulator);
        ArgumentNullException.ThrowIfNull(decisionService);
        ArgumentNullException.ThrowIfNull(decisionDataService);
        _eventBus = eventBus;
        _evaulator = evaulator;
        _decisionService = decisionService;
        _decisionDataService = decisionDataService;
    }

    public async Task<ServiceResponse> Handle(
        EvaluateSRsiCommand request,
        CancellationToken cancellationToken
    )
    {
        var results = _evaulator.GetSRSI(request.Quotes, SrsiSettings);
        if (!results.Any())
        {
            return new ServiceResponse(Result.Fail("Received empty rsi calculation"));
        }
        var last = results.Last();
        var additionalParams = new Dictionary<string, string>()
        {
            { nameof(last.StochK), last.StochK.ToString() }
        };
        var decision = _decisionService.MakeDecision(
            new IndexOutcome(IndexNames.Srsi, last.StochD.Value, additionalParams)
        );
        await _decisionDataService.Add(decision, cancellationToken);
        await _eventBus.Publish(decision, cancellationToken);
        return new ServiceResponse(Result.Ok());
    }
}

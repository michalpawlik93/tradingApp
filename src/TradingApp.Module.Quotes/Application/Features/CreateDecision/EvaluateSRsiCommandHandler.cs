using MediatR;
using TradingApp.Core.EventBus;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Application.Services;
using TradingApp.Module.Quotes.Domain.ValueObjects;
using TradingApp.Module.Quotes.Ports;

namespace TradingApp.Module.Quotes.Application.Features.CreateDecision;

/// <summary>
/// Evaluate decision for latest date in quotes
/// </summary>
/// <param name="Quotes"></param>
public record EvaluateSRsiCommand(List<Quote> Quotes) : IRequest;

public class EvaluateSRsiCommandHandler : IRequestHandler<EvaluateSRsiCommand>
{
    private readonly IEventBus _eventBus;
    private readonly IEvaluator _evaulator;
    private readonly IDecisionService _decisionService;

    public EvaluateSRsiCommandHandler(
        IEventBus eventBus,
        IEvaluator evaulator,
        IDecisionService decisionService
    )
    {
        ArgumentNullException.ThrowIfNull(eventBus);
        ArgumentNullException.ThrowIfNull(evaulator);
        ArgumentNullException.ThrowIfNull(evaulator);
        _eventBus = eventBus;
        _evaulator = evaulator;
        _decisionService = decisionService;
    }

    public async Task Handle(EvaluateSRsiCommand request, CancellationToken cancellationToken)
    {
        var sRsiSettings = new SRsiSettings(true, 12, 3, 3, -60, 60);
        var results = _evaulator.GetSRSI(new List<Quote>(), sRsiSettings);
        var last = results.Last();
        var decision = _decisionService.MakeDecision(new IndexOutcome("srsi", last.StochK.Value));
        //create in db
        await _eventBus.Publish(decision, cancellationToken);
    }
}
// i decision maker

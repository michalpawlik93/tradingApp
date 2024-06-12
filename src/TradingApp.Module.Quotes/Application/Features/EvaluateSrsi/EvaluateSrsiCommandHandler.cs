using FluentResults;
using MediatR;
using TradingApp.Core.EventBus;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Aggregates;

namespace TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;

/// <summary>
/// Evaluate decision for last date in quotes
/// </summary>
/// <param name="Quotes"></param>
public record EvaluateSRsiCommand(List<Quote> Quotes) : IRequest<IResultBase>;

public class EvaluateSRsiCommandHandler : IRequestHandler<EvaluateSRsiCommand, IResultBase>
{
    private readonly IEventBus _eventBus;
    private readonly IEvaluator _evaluator;
    private readonly ISrsiDecisionService _decisionService;
    private readonly IEntityDataService<Decision> _decisionDataService;

    private static SRsiSettings SrsiSettings = new(true, 12, 3, 3, -60, 60);

    public EvaluateSRsiCommandHandler(
        IEventBus eventBus,
        IEvaluator evaluator,
        ISrsiDecisionService decisionService,
        IEntityDataService<Decision> decisionDataService
    )
    {
        ArgumentNullException.ThrowIfNull(eventBus);
        ArgumentNullException.ThrowIfNull(evaluator);
        ArgumentNullException.ThrowIfNull(decisionService);
        ArgumentNullException.ThrowIfNull(decisionDataService);
        _eventBus = eventBus;
        _evaluator = evaluator;
        _decisionService = decisionService;
        _decisionDataService = decisionDataService;
    }

    public async Task<IResultBase> Handle(
        EvaluateSRsiCommand request,
        CancellationToken cancellationToken
    )
    {
        var results = _evaluator.GetSRSI(request.Quotes, SrsiSettings);
        if (results.Count == 0)
        {
            return Result.Fail("Received empty rsi calculation");
        }
        var decision = _decisionService.MakeDecision(results);
        await _decisionDataService.Add(decision, cancellationToken);
        await _eventBus.Publish(decision, cancellationToken);
        return Result.Ok();
    }
}

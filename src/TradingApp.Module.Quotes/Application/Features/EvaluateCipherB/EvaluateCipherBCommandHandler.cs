using FluentResults;
using MediatR;
using TradingApp.Core.EventBus;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Aggregates;

namespace TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;

public record EvaluateCipherBCommand(
    IEnumerable<CypherBQuote> Quotes,
    Granularity Granularity,
    WaveTrendSettings WaveTrendSettings
) : IRequest<IResultBase>;

public class EvaluateCipherBCommandHandler : IRequestHandler<EvaluateCipherBCommand, IResultBase>
{
    private readonly IEventBus _eventBus;
    private readonly ICypherBDecisionService _decisionService;
    private readonly IEntityDataService<Decision> _decisionDataService;

    public EvaluateCipherBCommandHandler(
        IEventBus eventBus,
        ICypherBDecisionService decisionService,
        IEntityDataService<Decision> decisionDataService
    )
    {
        ArgumentNullException.ThrowIfNull(eventBus);
        ArgumentNullException.ThrowIfNull(decisionService);
        ArgumentNullException.ThrowIfNull(decisionDataService);
        _eventBus = eventBus;
        _decisionService = decisionService;
        _decisionDataService = decisionDataService;
    }

    public async Task<IResultBase> Handle(
        EvaluateCipherBCommand request,
        CancellationToken cancellationToken
    )
    {
        var decision = _decisionService.MakeDecision(
            new CypherBDecisionRequest(request.Quotes, request.Granularity, request.WaveTrendSettings)
        );
        if (decision.IsFailed)
        {
            return decision;
        }
        await _decisionDataService.Add(decision.Value, cancellationToken);
        await _eventBus.Publish(decision.Value, cancellationToken);
        return Result.Ok();
    }
}

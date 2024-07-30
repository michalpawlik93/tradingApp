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
public record EvaluateSRsiCommand(
    IReadOnlyList<Quote> Quotes,
    SrsiDecisionSettings SrsiDecisionSettings,
    SRsiSettings SRsiSetting
) : IRequest<IResultBase>;

public class EvaluateSRsiCommandHandler : IRequestHandler<EvaluateSRsiCommand, IResultBase>
{
    private readonly IEventBus _eventBus;
    private readonly ISrsiDecisionService _decisionService;
    private readonly IEntityDataService<Decision> _decisionDataService;

    public EvaluateSRsiCommandHandler(
        IEventBus eventBus,
        ISrsiDecisionService decisionService,
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
        EvaluateSRsiCommand request,
        CancellationToken cancellationToken
    )
    {
        var decisionResult = _decisionService.MakeDecision(
            request.Quotes,
            request.SrsiDecisionSettings,
            request.SRsiSetting
        );
        if (decisionResult.IsFailed)
        {
            return decisionResult;
        }
        await _decisionDataService.Add(decisionResult.Value, cancellationToken);
        await _eventBus.Publish(decisionResult.Value, cancellationToken);
        return Result.Ok();
    }
}

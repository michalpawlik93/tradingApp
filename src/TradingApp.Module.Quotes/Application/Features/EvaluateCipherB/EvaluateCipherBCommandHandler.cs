using FluentResults;
using MediatR;
using System.Globalization;
using TradingApp.Core.EventBus;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Application.Services;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Constants;
using TradingApp.Module.Quotes.Domain.ValueObjects;

namespace TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;

public record EvaluateCipherBCommand(IEnumerable<CypherBQuote> Quotes) : IRequest<IResultBase>;

public class EvaluateCipherBCommandHandler : IRequestHandler<EvaluateCipherBCommand, IResultBase>
{
    private readonly IEventBus _eventBus;
    private readonly IDecisionService _decisionService;
    private readonly IEntityDataService<Decision> _decisionDataService;


    public EvaluateCipherBCommandHandler(
        IEventBus eventBus,
        IDecisionService decisionService,
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
        var latestQuote = request.Quotes.Last();
        var additionalParams = new Dictionary<string, string>
        {
            { nameof(latestQuote.WaveTrend.Wt2), latestQuote.WaveTrend.Wt2.ToString(CultureInfo.InvariantCulture) },
            { nameof(latestQuote.WaveTrend.Vwap), latestQuote.WaveTrend.Vwap.ToString() },
            { nameof(latestQuote.Mfi.Mfi), latestQuote.Mfi.Mfi.ToString(CultureInfo.InvariantCulture) }
        };
        var decision = _decisionService.MakeDecision(
            new IndexOutcome(IndexNames.CipherB, latestQuote.WaveTrend.Wt1, additionalParams)
        );
        await _decisionDataService.Add(decision, cancellationToken);
        await _eventBus.Publish(decision, cancellationToken);
        return Result.Ok();
    }
}

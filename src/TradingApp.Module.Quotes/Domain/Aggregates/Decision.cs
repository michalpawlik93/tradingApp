using TradingApp.Core.Domain;
using TradingApp.Core.EventBus.Events;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.Events.Decision;
using TradingApp.Module.Quotes.Domain.ValueObjects;

namespace TradingApp.Module.Quotes.Domain.Aggregates;

public class Decision : AggregateRoot<DecisionId>
{
    public IndexOutcome IndexOutcome { get; }
    public DateTime TimeStamp { get; }
    public TradeAction Action { get; }

    public MarketDirection MarketDirection { get; }

    public static Decision CreateNew(
        IndexOutcome indexOutcome,
        DateTime timeStamp,
        TradeAction action,
        MarketDirection marketDirection
    )
    {
        return new Decision(indexOutcome, timeStamp, action, marketDirection);
    }

    private Decision() { }

    private Decision(
        IndexOutcome indexOutcome,
        DateTime timeStamp,
        TradeAction action,
        MarketDirection marketDirection
    ) : base(DecisionId.NewId())
    {
        IndexOutcome = indexOutcome;
        TimeStamp = timeStamp;
        Action = action;
        MarketDirection = marketDirection;
        AddDomainEvent(new DecisionCreatedDomainEvent(Id));
        AddIntegrationEvent(new DecisionCreatedIntegrationEvent(Id.ToGuid()));
    }

    private Decision(
        Guid id,
        IndexOutcome indexOutcome,
        DateTime timeStamp,
        TradeAction action,
        MarketDirection marketDirection
    ) : base(DecisionId.Clone(id))
    {
        IndexOutcome = indexOutcome;
        TimeStamp = timeStamp;
        Action = action;
        MarketDirection = marketDirection;
    }

    public static Decision Clone(
        Guid id,
        IndexOutcome indexOutcome,
        DateTime timeStamp,
        TradeAction action,
        MarketDirection marketDirection
    )
    {
        return new Decision(id, indexOutcome, timeStamp, action, marketDirection);
    }
}

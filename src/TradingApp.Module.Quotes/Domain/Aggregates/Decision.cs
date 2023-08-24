using TradingApp.Core.Domain;
using TradingApp.Core.EventBus.Events;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.Events.Decision;
using TradingApp.Module.Quotes.Domain.ValueObjects;

namespace TradingApp.Module.Quotes.Domain.Aggregates
{
    public class Decision : AggregateRoot<DecisionId>
    {
        public IndexOutcome IndexOutcome { get; }
        public DateTime TimeStamp { get; }
        public TradeAction Action { get; }

        public SignalStrength SignalStrength { get; }
        public MarketDirection MarketDirection { get; }


        internal static Decision CreateNew(
            IndexOutcome indexOutcome,
            DateTime timeStamp,
            TradeAction action,
            SignalStrength signalStrength,
            MarketDirection marketDirection
        )
        {
            return new Decision(indexOutcome, timeStamp, action, signalStrength, marketDirection);
        }

        private Decision() { }

        private Decision(
            IndexOutcome indexOutcome,
            DateTime timeStamp,
            TradeAction action,
            SignalStrength signalStrength,
            MarketDirection marketDirection
        ) : base(DecisionId.NewId())
        {
            IndexOutcome = indexOutcome;
            TimeStamp = timeStamp;
            Action = action;
            SignalStrength = signalStrength;
            MarketDirection = marketDirection;
            AddDomainEvent(new DecisionCreatedDomainEvent(Id));
            AddIntegrationEvent(new DecisionCreatedIntegrationEvent(Id.ToGuid()));
        }
    }
}

using TradingApp.Core.Domain;
using TradingApp.Modules.Domain.Enums;
using TradingApp.Modules.Domain.Events.Decision;
using TradingApp.Modules.Domain.ValueObjects;

namespace TradingApp.Modules.Domain.Aggregates
{
    public class Decision : AggregateRoot<DecisionId>
    {
        public IndexOutcome IndexName { get; }
        public DateTime TimeStamp { get; }
        public TradeAction Action { get; }

        public SignalStrength SignalStrength { get; }
        public MarketDirection MarketDirection { get; }


        internal static Decision CreateNew(
            IndexOutcome indexName,
            DateTime timeStamp,
            TradeAction action,
            SignalStrength signalStrength,
            MarketDirection marketDirection
        )
        {
            return new Decision(indexName, timeStamp, action, signalStrength, marketDirection);
        }

        private Decision() { }

        private Decision(
            IndexOutcome indexName,
            DateTime timeStamp,
            TradeAction action,
            SignalStrength signalStrength,
            MarketDirection marketDirection
        ) : base(DecisionId.NewId())
        {
            IndexName = indexName;
            TimeStamp = timeStamp;
            Action = action;
            SignalStrength = signalStrength;
            MarketDirection = marketDirection;
            AddDomainEvent(new DecisionCreatedDomainEvent(Id));
        }
    }
}

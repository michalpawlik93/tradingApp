﻿using TradingApp.Core.Domain;
using TradingApp.Modules.Quotes.Domain.Enums;
using TradingApp.Modules.Quotes.Domain.Events.Decision;
using TradingApp.Modules.Quotes.Domain.ValueObjects;

namespace TradingApp.Modules.Quotes.Domain.Aggregates
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
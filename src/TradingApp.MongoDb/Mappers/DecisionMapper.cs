using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;
using TradingApp.MongoDb.Models;

namespace TradingApp.MongoDb.Mappers;

public class DecisionMapper : IMongoDbMapper<Decision, DecisionDao>
{
    public DecisionDao ToDao(Decision domain)
    {
        return new DecisionDao()
        {
            Id = domain.Id.Id,
            Action = domain.Action.ToString(),
            MarketDirection = domain.MarketDirection.ToString(),
            TimeStamp = new DateTimeOffset(domain.TimeStamp),
            SignalStrengthLevel = domain.SignalStrength.StrengthLevel.ToString(),
            SignalStrengthValue = domain.SignalStrength.Value,
            IndexOutcomeName = domain.IndexOutcome.Name,
            IndexOutcomeValue = domain.IndexOutcome.Value,
        };
    }

    public Decision ToDomain(DecisionDao dao)
    {
        var indexOutcome = new IndexOutcome(dao.IndexOutcomeName, dao.IndexOutcomeValue);

        var signalStrength = new SignalStrength(
            dao.SignalStrengthValue,
            GetEnumValue<SignalStrengthLevel>(dao.SignalStrengthLevel!)
        );
        return Decision.Clone(
            dao.Id,
            indexOutcome,
            dao.TimeStamp.UtcDateTime,
            GetEnumValue<TradeAction>(dao.Action!),
            signalStrength,
            GetEnumValue<MarketDirection>(dao.MarketDirection!)
        );
    }

    private T GetEnumValue<T>(string value) where T : struct, Enum
    {
        if (!Enum.TryParse(value, out T enumValue))
        {
            throw new ArgumentException($"Invalid {typeof(T).Name} value: {value}");
        }

        return enumValue;
    }
}

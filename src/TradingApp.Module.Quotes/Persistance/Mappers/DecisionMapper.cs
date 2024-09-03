using FluentResults;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;
using TradingApp.Module.Quotes.Persistance.Models;

namespace TradingApp.Module.Quotes.Persistance.Mappers;

public class DecisionMapper : IMongoDbMapper<Decision, DecisionDao>
{
    public Result<DecisionDao> ToDao(Decision domain)
    {
        return Result.Ok(
            new DecisionDao()
            {
                Id = domain.Id.Id,
                Action = domain.Action.ToString(),
                MarketDirection = domain.MarketDirection.ToString(),
                TimeStamp = new DateTimeOffset(domain.TimeStamp),
                IndexOutcomeName = domain.IndexOutcome.Name,
                IndexOutcomeValue = domain.IndexOutcome.Value ?? default,
            }
        );
    }

    public Result<Decision> ToDomain(DecisionDao dao)
    {
        var tradeActionResult = GetEnumValue<TradeAction>(dao.Action!);
        if (tradeActionResult.IsFailed)
        {
            return tradeActionResult.ToResult();
        }
        var marketDirectionResult = GetEnumValue<MarketDirection>(dao.MarketDirection!);
        if (marketDirectionResult.IsFailed)
        {
            return marketDirectionResult.ToResult();
        }

        var indexOutcome = new IndexOutcome(dao.IndexOutcomeName, dao.IndexOutcomeValue);

        return Decision.Clone(
            dao.Id,
            indexOutcome,
            dao.TimeStamp.UtcDateTime,
            tradeActionResult.Value,
            marketDirectionResult.Value
        );
    }

    private static Result<T> GetEnumValue<T>(string value) where T : struct, Enum
    {
        if (!Enum.TryParse(value, out T enumValue))
        {
            return Result.Fail(new ValidationError($"Invalid {typeof(T).Name} value: {value}"));
        }

        return enumValue;
    }
}

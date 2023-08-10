using TradingApp.Core.Domain;

namespace TradingApp.Modules.Quotes.Domain.ValueObjects;

public class DecisionId : Identifier<DecisionId, Guid>
{
    protected DecisionId(Guid id) : base(id) { }

    public static DecisionId NewId() => new(Guid.NewGuid());

    public override DecisionId Copy() => new(Id);
}

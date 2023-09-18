using TradingApp.Core.Domain;

namespace TradingApp.Module.Quotes.Domain.ValueObjects;

public class DecisionId : Identifier<DecisionId, Guid>
{
    protected DecisionId(Guid id) : base(id) { }

    public static DecisionId NewId() => new(Guid.NewGuid());

    public static DecisionId Clone(Guid id) => new(id);

    public Guid ToGuid() => Id;
}

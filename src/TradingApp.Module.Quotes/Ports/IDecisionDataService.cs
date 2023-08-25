using TradingApp.Module.Quotes.Domain.Aggregates;

namespace TradingApp.Module.Quotes.Ports;

public interface IDecisionDataService
{
    Task Add(Decision decision, CancellationToken cancellationToken);
}

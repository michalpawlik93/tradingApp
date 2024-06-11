using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.ValueObjects;

namespace TradingApp.Module.Quotes.Application.Services
{
    public class CypherBDecisionService : IDecisionService
    {
        public Decision MakeDecision(IndexOutcome indexOutcome)
        {
            // CipherB strategy
            throw new NotImplementedException();
        }
    }
}

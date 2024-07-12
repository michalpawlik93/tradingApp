using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Constants;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;

namespace TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;

public interface ISrsiDecisionService
{
    Decision MakeDecision(IEnumerable<SRsiResult> quotes);
}


public class SrsiDecisionService : ISrsiDecisionService
{
    public Decision MakeDecision(IEnumerable<SRsiResult> quotes)
    {
        var last = quotes.Last();
        var additionalParams = new Dictionary<string, string>()
         {
             { nameof(last.StochK), last.StochK.ToString() }
         };
        var decision = Decision.CreateNew(new IndexOutcome(IndexNames.Srsi, last.StochD.Value, additionalParams), DateTime.UtcNow, TradeAction.Buy, MarketDirection.Bullish);
        return decision;
    }
}

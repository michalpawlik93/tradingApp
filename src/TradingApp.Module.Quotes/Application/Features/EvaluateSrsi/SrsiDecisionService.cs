using FluentResults;
using System.Globalization;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Constants;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;

namespace TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;

public record struct SrsiDecisionSettings(
    SRsiSettings SrsiSettings,
    int EmaLength,
    Granularity Granularity,
    TradingStrategy TradingStrategy
);

public interface ISrsiDecisionService
{
    /// <summary>
    /// Get Decision for latest date
    /// </summary>
    /// <param name="quotes"></param>
    /// <param name="srsiDecisionSettings"></param>
    /// <param name="sRsiSettings"></param>
    /// <returns></returns>
    Result<Decision> MakeDecision(
        IReadOnlyList<Quote> quotes,
        SrsiDecisionSettings srsiDecisionSettings
    );
}

public class SrsiDecisionService : ISrsiDecisionService
{
    private readonly ISrsiStrategyFactory _srsiStrategyFactory;

    public SrsiDecisionService(ISrsiStrategyFactory srsiStrategyFactory)
    {
        ArgumentNullException.ThrowIfNull(srsiStrategyFactory);
        _srsiStrategyFactory = srsiStrategyFactory;
    }

    public Result<Decision> MakeDecision(
        IReadOnlyList<Quote> quotes,
        SrsiDecisionSettings srsiDecisionSettings
    )
    {
        var strategy = _srsiStrategyFactory.GetStrategy(
            srsiDecisionSettings.TradingStrategy,
            srsiDecisionSettings.Granularity
        );
        var signals = strategy.EvaluateSignals(quotes);
        if (signals.IsFailed)
        {
            return signals.ToResult();
        }
        var last = signals.Value[^1];
        var additionalParams = new Dictionary<string, string>
        {
            { nameof(last.StochK), last.StochK.ToString(CultureInfo.InvariantCulture) },
            { nameof(last.StochD), last.StochD.ToString(CultureInfo.InvariantCulture) }
        };
        return Decision.CreateNew(
            new IndexOutcome(IndexNames.Srsi, null, additionalParams),
            DateTime.UtcNow,
            signals.Value[^1].TradeAction,
            MarketDirection.Bullish
        );
    }
}

//https://www.liteforex.pl/blog/for-beginners/najlepsze-wskazniki-forex/oscylator-stochastyczny/

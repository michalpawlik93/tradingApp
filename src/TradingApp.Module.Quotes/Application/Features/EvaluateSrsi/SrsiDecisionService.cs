﻿using FluentResults;
using TradingApp.Module.Quotes.Application.Features.TradeSignals;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Constants;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;

namespace TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;

public record struct SrsiDecisionSettings(SRsiSettings SrsiSettings, int EmaLength);

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

    /// <summary>
    /// Get list of decisions in the past
    /// </summary>
    /// <param name="quotes"></param>
    /// <param name="srsiDecisionSettings"></param>
    /// <param name="sRsiSettings"></param>
    /// <returns></returns>
    Result<IReadOnlyList<SrsiSignal>> GetDecisionQuotes(
        IReadOnlyList<Quote> quotes,
        SrsiDecisionSettings srsiDecisionSettings
    );
}

public class SrsiDecisionService : ISrsiDecisionService
{
    private readonly IEvaluator _evaluator;

    public SrsiDecisionService(IEvaluator evaluator)
    {
        ArgumentNullException.ThrowIfNull(evaluator);
        _evaluator = evaluator;
    }

    public Result<Decision> MakeDecision(
        IReadOnlyList<Quote> quotes,
        SrsiDecisionSettings srsiDecisionSettings
    )
    {
        var srsiResults = _evaluator.GetSrsi(quotes, srsiDecisionSettings.SrsiSettings);
        if (srsiResults.Count < 2)
        {
            return Result.Fail("Quotes can not be less than 2 elements");
        }
        var last = srsiResults[^1];
        var penult = srsiResults[^2];
        var additionalParams = new Dictionary<string, string>
        {
            { nameof(last.StochK), last.StochK.ToString() },
            { nameof(last.StochD), last.StochD.ToString() }
        };
        var closePrices = quotes.Select(x => x.Close).ToArray();
        var ema = _evaluator.GetEmea(closePrices, srsiDecisionSettings.EmaLength);
        if (ema.IsFailed)
        {
            return ema.ToResult();
        }
        var ema2X = _evaluator.GetEmea(closePrices, srsiDecisionSettings.EmaLength * 2);
        if (ema2X.IsFailed)
        {
            return ema2X.ToResult();
        }
        var decision = Decision.CreateNew(
            new IndexOutcome(IndexNames.Srsi, null, additionalParams),
            DateTime.UtcNow,
            SrsiSignals.GetTradeAction(
                quotes[^1].Close,
                last,
                penult,
                srsiDecisionSettings.SrsiSettings,
                ema.Value[^1],
                ema2X.Value[^1]
            ),
            MarketDirection.Bullish
        );
        return decision;
    }

    public Result<IReadOnlyList<SrsiSignal>> GetDecisionQuotes(
        IReadOnlyList<Quote> quotes,
        SrsiDecisionSettings srsiDecisionSettings
    )
    {
        var results = _evaluator.GetSrsi(quotes, srsiDecisionSettings.SrsiSettings);
        var closePrices = quotes.Select(x => x.Close).ToArray();
        var ema = _evaluator.GetEmea(closePrices, srsiDecisionSettings.EmaLength);
        if (ema.IsFailed)
        {
            return ema.ToResult();
        }
        var ema2X = _evaluator.GetEmea(closePrices, srsiDecisionSettings.EmaLength * 2);
        if (ema2X.IsFailed)
        {
            return ema2X.ToResult();
        }
        var signals = SrsiSignals.CreateSriSignals(
            quotes,
            results,
            srsiDecisionSettings.SrsiSettings,
            ema.Value,
            ema2X.Value
        );
        return Result.Ok(signals);
    }
}

//https://www.liteforex.pl/blog/for-beginners/najlepsze-wskazniki-forex/oscylator-stochastyczny/

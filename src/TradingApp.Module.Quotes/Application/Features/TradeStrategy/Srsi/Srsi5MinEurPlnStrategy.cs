using FluentResults;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Enums;

namespace TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;

// todo: add SideIndices as part of request
public class Srsi5MinEurPlnStrategy : ISrsiStrategy
{
    private readonly IEvaluator _evaluator;

    private static SrsiSettings FastSettings => new(true, 3, 14, 3, 10, 90);
    private const int Ema = 50;
    private const int Ema2X = 100;
    private const int DecimalPlace = 4;

    public Srsi5MinEurPlnStrategy(IEvaluator evaluator)
    {
        ArgumentNullException.ThrowIfNull(evaluator);
        _evaluator = evaluator;
    }

    /// <summary>
    /// Stoch with Ema
    /// Long
    ///  - %K crosses %D down to up
    ///  - %K and %D are below oversold level
    ///  Short
    ///  - %K crosses %D up to down
    ///  - %K and %D are above overbought level
    /// </summary>
    /// <returns></returns>
    public Result<IReadOnlyList<SrsiSignal>> EvaluateSignals(
        IReadOnlyList<Quote> quotes,
        SrsiSettings? customSettings = null
    )
    {
        if (customSettings is { Enabled: false })
        {
            return Result.Ok((IReadOnlyList<SrsiSignal>)[]);
        }
        var srsiResults = _evaluator.GetSrsi(quotes, customSettings ?? FastSettings);
        if (srsiResults.Count < 2)
        {
            return Result.Fail("Quotes can not be less than 2 elements");
        }
        var closePrices = quotes.Select(x => x.Close).ToArray();
        var ema = _evaluator.GetEmea(closePrices, Ema);
        if (ema.IsFailed)
        {
            return ema.ToResult();
        }
        var ema2X = _evaluator.GetEmea(closePrices, Ema2X * 2);
        if (ema2X.IsFailed)
        {
            return ema2X.ToResult();
        }

        var signals = CreateSriSignals(
            quotes,
            srsiResults,
            customSettings ?? FastSettings,
            ema.Value,
            ema2X.Value
        );
        return Result.Ok(signals);
    }

    private static IReadOnlyList<SrsiSignal> CreateSriSignals(
        IReadOnlyList<Quote> quotes,
        IReadOnlyList<SRsiResult> srsiResults,
        SrsiSettings sRsiSettings,
        IReadOnlyList<decimal> ema,
        IReadOnlyList<decimal> ema2X
    )
    {
        var results = new List<SrsiSignal>(srsiResults.Count);
        for (var i = 0; i < srsiResults.Count; i++)
        {
            if (i == 0)
            {
                results.Add(new SrsiSignal(0, 0, TradeAction.Hold));
                continue;
            }
            results.Add(
                srsiResults[i].StochD.HasValue
                && srsiResults[i].StochK.HasValue
                && srsiResults[i - 1].StochD.HasValue
                && srsiResults[i - 1].StochK.HasValue
                    ? new SrsiSignal(
                        Math.Round(srsiResults[i].StochK.Value, DecimalPlace),
                        Math.Round(srsiResults[i].StochD.Value, DecimalPlace),
                        GetTradeAction(
                            quotes[i].Close,
                            srsiResults[i],
                            srsiResults[i - 1],
                            sRsiSettings,
                            ema[i],
                            ema2X[i]
                        )
                    )
                    : new SrsiSignal(0, 0, TradeAction.Hold)
            );
        }

        return results;
    }

    public static TradeAction GetTradeAction(
        decimal latestClose,
        SRsiResult last,
        SRsiResult penult,
        SrsiSettings sRsiSettings,
        decimal ema,
        decimal ema2X
    )
    {
        if (SellSignal(latestClose, last, penult, sRsiSettings, ema, ema2X))
        {
            return TradeAction.Sell;
        }
        return BuySignal(latestClose, last, penult, sRsiSettings, ema, ema2X)
            ? TradeAction.Buy
            : TradeAction.Hold;
    }

    private static bool SellSignal(
        decimal latestClose,
        SRsiResult last,
        SRsiResult penult,
        SrsiSettings sRsiSettings,
        decimal ema,
        decimal ema2X
    ) =>
        SrsiStrategyExtensions.KDSellSignal(last, penult, sRsiSettings)
        && !SrsiStrategyExtensions.KDBuySignal(last, penult, sRsiSettings)
        && EmaSell(ema, ema2X)
        && ClosePriceSell(latestClose, ema);

    private static bool BuySignal(
        decimal latestClose,
        SRsiResult last,
        SRsiResult penult,
        SrsiSettings sRsiSettings,
        decimal ema,
        decimal ema2X
    ) =>
        SrsiStrategyExtensions.KDBuySignal(last, penult, sRsiSettings)
        && !SrsiStrategyExtensions.KDSellSignal(last, penult, sRsiSettings)
        && EmaBuy(ema, ema2X)
        && ClosePriceBuy(latestClose, ema);

    private static bool EmaSell(decimal ema, decimal ema2X) => ema < ema2X;

    private static bool ClosePriceSell(decimal latestClose, decimal ema) => latestClose < ema;

    private static bool ClosePriceBuy(decimal latestClose, decimal ema) => latestClose > ema;

    private static bool EmaBuy(decimal ema, decimal ema2X) => ema > ema2X;
}

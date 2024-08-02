using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Enums;

namespace TradingApp.Module.Quotes.Application.Features.TradeSignals;

public static class SrsiSignals
{
    private const int DecimalPlace = 4;

    public static IReadOnlyList<SrsiSignal> CreateSriSignals(
        IReadOnlyList<Quote> quotes,
        IReadOnlyList<SRsiResult> srsiResults,
        SRsiSettings sRsiSettings,
        IReadOnlyList<decimal> ema,
        IReadOnlyList<decimal> ema2X
    )
    {
        if (srsiResults.Count < 2)
        {
            return new List<SrsiSignal>(0);
        }
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
        SRsiSettings sRsiSettings,
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
        SRsiSettings sRsiSettings,
        decimal ema,
        decimal ema2X
    ) =>
        KSDellSignal(last, penult, sRsiSettings)
        && !KDBuySignal(last, penult, sRsiSettings)
        && EmaSell(ema, ema2X)
        && ClosePriceSell(latestClose, ema);

    private static bool BuySignal(
        decimal latestClose,
        SRsiResult last,
        SRsiResult penult,
        SRsiSettings sRsiSettings,
        decimal ema,
        decimal ema2X
    ) =>
        KDBuySignal(last, penult, sRsiSettings)
        && !KSDellSignal(last, penult, sRsiSettings)
        && EmaBuy(ema, ema2X)
        && ClosePriceBuy(latestClose, ema);

    private static bool KSDellSignal(
        SRsiResult last,
        SRsiResult penult,
        SRsiSettings sRsiSettings
    ) =>
        penult.StochK > sRsiSettings.Overbought
        && last.StochK < sRsiSettings.Overbought
        && penult.StochK > penult.StochD
        && last.StochK < last.StochD;

    private static bool KDBuySignal(
        SRsiResult last,
        SRsiResult penult,
        SRsiSettings sRsiSettings
    ) =>
        penult.StochK < sRsiSettings.Oversold
        && last.StochK > sRsiSettings.Oversold
        && penult.StochK < penult.StochD
        && last.StochK > last.StochD;

    private static bool EmaSell(decimal ema, decimal ema2X) => ema < ema2X;

    private static bool ClosePriceSell(decimal latestClose, decimal ema) => latestClose < ema;

    private static bool ClosePriceBuy(decimal latestClose, decimal ema) => latestClose > ema;

    private static bool EmaBuy(decimal ema, decimal ema2X) => ema > ema2X;
}

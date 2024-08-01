using TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Enums;

namespace TradingApp.Module.Quotes.Application.Features.TradeSignals;

public static class SrsiSignals
{
    public static TradeAction GetTradeAction(
        decimal latestClose,
        SRsiResult last,
        SRsiResult penult,
        SrsiDecisionSettings srsiDecisionSettings
    ) =>
        (latestClose, last, penult, srsiDecisionSettings) switch
        {
            var (c, l, p, sd) when SellSignal(c, l, p, sd) => TradeAction.Sell,
            var (c, l, p, sd) when BuySignal(c, l, p, sd) => TradeAction.Buy,
            _ => TradeAction.Hold
        };

    private static bool SellSignal(
        decimal latestClose,
        SRsiResult last,
        SRsiResult penult,
        SrsiDecisionSettings srsiDecisionSettings
    ) =>
        KSDellSignal(last, penult, srsiDecisionSettings.SrsiSettings)
        && !KDBuySignal(last, penult, srsiDecisionSettings.SrsiSettings)
        && EmaSell(srsiDecisionSettings)
        && ClosePriceSell(latestClose, srsiDecisionSettings);

    private static bool BuySignal(
        decimal latestClose,
        SRsiResult last,
        SRsiResult penult,
        SrsiDecisionSettings srsiDecisionSettings
    ) =>
        KDBuySignal(last, penult, srsiDecisionSettings.SrsiSettings)
        && !KSDellSignal(last, penult, srsiDecisionSettings.SrsiSettings)
        && EmaBuy(srsiDecisionSettings)
        && ClosePriceBuy(latestClose, srsiDecisionSettings);

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

    private static bool EmaSell(SrsiDecisionSettings srsiDecisionSettings) =>
        srsiDecisionSettings.Ema < srsiDecisionSettings.Ema2X;

    private static bool ClosePriceSell(
        decimal latestClose,
        SrsiDecisionSettings srsiDecisionSettings
    ) => latestClose < srsiDecisionSettings.Ema;

    private static bool ClosePriceBuy(
        decimal latestClose,
        SrsiDecisionSettings srsiDecisionSettings
    ) => latestClose > srsiDecisionSettings.Ema;

    private static bool EmaBuy(SrsiDecisionSettings srsiDecisionSettings) =>
        srsiDecisionSettings.Ema > srsiDecisionSettings.Ema2X;
}

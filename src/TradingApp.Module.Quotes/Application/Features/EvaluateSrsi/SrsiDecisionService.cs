using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Aggregates;
using TradingApp.Module.Quotes.Domain.Constants;
using TradingApp.Module.Quotes.Domain.Enums;
using TradingApp.Module.Quotes.Domain.ValueObjects;

namespace TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;

public record SrsiQuote(Quote Ohlc, SRsiResult SRsiResult, decimal Ema, decimal Ema2X);

public interface ISrsiDecisionService
{
    Decision MakeDecision(IEnumerable<SrsiQuote> quotes, SRsiSettings sRsiSettings);
}

public class SrsiDecisionService : ISrsiDecisionService
{
    public Decision MakeDecision(IEnumerable<SrsiQuote> quotes, SRsiSettings sRsiSettings)
    {
        var last = quotes.Last();
        var penult = quotes.ElementAt(quotes.Count() - 2);
        var additionalParams = new Dictionary<string, string>
        {
            { nameof(last.SRsiResult.StochK), last.SRsiResult.StochK.ToString() },
            { nameof(last.SRsiResult.StochD), last.SRsiResult.StochD.ToString() }
        };
        var decision = Decision.CreateNew(
            new IndexOutcome(IndexNames.Srsi, null, additionalParams),
            DateTime.UtcNow,
            GetTradeAction(last, penult, sRsiSettings),
            MarketDirection.Bullish
        );
        return decision;
    }

    private static TradeAction GetTradeAction(
        SrsiQuote last,
        SrsiQuote penult,
        SRsiSettings sRsiSettings
    ) =>
        (last, penult, sRsiSettings) switch
        {
            var (l, p, s) when SellSignal(l, p, s) => TradeAction.Sell,
            var (l, p, s) when BuySignal(l, p, s) => TradeAction.Buy,
            _ => TradeAction.Hold
        };

    private static bool SellSignal(SrsiQuote last, SrsiQuote penult, SRsiSettings sRsiSettings) =>
        KSDellSignal(last, penult, sRsiSettings)
        && !KDBuySignal(last, penult, sRsiSettings)
        && EmaSell(last, penult)
        && ClosePriceSell(last);

    private static bool BuySignal(SrsiQuote last, SrsiQuote penult, SRsiSettings sRsiSettings) =>
        !KSDellSignal(last, penult, sRsiSettings)
        && KDBuySignal(last, penult, sRsiSettings)
        && EmaBuy(last, penult)
        && ClosePriceBuy(last);

    private static bool KSDellSignal(SrsiQuote last, SrsiQuote penult, SRsiSettings sRsiSettings) =>
        penult.SRsiResult.StochK > sRsiSettings.Overbought
        && last.SRsiResult.StochK < sRsiSettings.Overbought
        && penult.SRsiResult.StochK > penult.SRsiResult.StochD
        && last.SRsiResult.StochK < last.SRsiResult.StochD;

    private static bool KDBuySignal(SrsiQuote last, SrsiQuote penult, SRsiSettings sRsiSettings) =>
        penult.SRsiResult.StochK < sRsiSettings.Oversold
        && last.SRsiResult.StochK > sRsiSettings.Oversold
        && penult.SRsiResult.StochK < penult.SRsiResult.StochD
        && last.SRsiResult.StochK > last.SRsiResult.StochD;

    private static bool EmaSlopingDownward(SrsiQuote last, SrsiQuote penult) =>
        penult.Ema > last.Ema && penult.Ema2X > last.Ema2X;

    private static bool EmaSlopingUpward(SrsiQuote last, SrsiQuote penult) =>
        penult.Ema < last.Ema && penult.Ema2X < last.Ema2X;

    private static bool EmaSell(SrsiQuote last, SrsiQuote penult) =>
        last.Ema < last.Ema2X && EmaSlopingDownward(last, penult);

    private static bool ClosePriceSell(SrsiQuote last) => last.Ohlc.Close < last.Ema;

    private static bool ClosePriceBuy(SrsiQuote last) => last.Ohlc.Close > last.Ema;

    private static bool EmaBuy(SrsiQuote last, SrsiQuote penult) =>
        last.Ema > last.Ema2X && EmaSlopingUpward(last, penult);
}

// strategy must link index settings provided to index with decision where levels are crucial
// smae settings must go to evaluator and to decision service
//Strategy class can contain decision service and index calculation, then im certain same settings are across all calculations
public static class SrsiStrategies
{
    // day trading strategy
    // scalping strategy

}

//https://www.liteforex.pl/blog/for-beginners/najlepsze-wskazniki-forex/oscylator-stochastyczny/



using FluentResults;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;

namespace TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;

public interface ISrsiStrategy
{
    Result<IReadOnlyList<SrsiSignal>> EvaluateSignals(
        IReadOnlyList<Quote> quotes,
        SrsiSettings? customSettings = null
    );
}

public interface ISrsiStrategyFactory
{
    ISrsiStrategy GetStrategy(AssetName assetName, Granularity granularity);
}

public class SrsiStrategyFactory : ISrsiStrategyFactory
{
    private readonly IEvaluator _evaluator;

    public SrsiStrategyFactory(IEvaluator evaluator)
    {
        ArgumentNullException.ThrowIfNull(evaluator);
        _evaluator = evaluator;
    }

    public ISrsiStrategy GetStrategy(AssetName assetName, Granularity granularity) =>
        (assetName, granularity) switch
        {
            (AssetName.EURPLN, Granularity.FiveMins) => new Srsi5MinEurPlnStrategy(_evaluator),
            (AssetName.EURPLN, Granularity.Hourly) => new Srsi1hEurPlnStrategy(_evaluator),
            _ => new SrsiDefaultStrategy(_evaluator),
        };
}

public static class SrsiStrategyExtensions
{
    public static bool KDSellSignal(
        SRsiResult last,
        SRsiResult penult,
        SrsiSettings sRsiSettings
    ) =>
        (penult.StochK >= sRsiSettings.Overbought && last.StochK <= sRsiSettings.Overbought)
            && penult.StochK > penult.StochD
            && last.StochK < last.StochD;

    public static bool KDBuySignal(SRsiResult last, SRsiResult penult, SrsiSettings sRsiSettings) =>
        penult.StochK <= sRsiSettings.Oversold
        && last.StochK >= sRsiSettings.Oversold
        && penult.StochK < penult.StochD
        && last.StochK > last.StochD;
}

/*
 Parametry wejsciowe to:
Granularity (e.g., 5 min, 30 min, 1 hour, 1 day)
Indicators (e.g., SRSI, MACD, RSI, Stochastic)
SideIndicators(MACD, EMA,etc)
Market Condition (e.g., Low Volatility, High Volatility)
Instrument (EURUSD)

Odpowiednia kombinacja daje nazwe strategi:
NazwaIndeksu + Granularity + MarketVolatility + Instrument

każda strategia musi uwzglednia wskaźniki pomocnicze
 * */
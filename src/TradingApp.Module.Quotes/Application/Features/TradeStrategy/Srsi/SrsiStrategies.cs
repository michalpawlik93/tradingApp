using FluentResults;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;

namespace TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;

public interface ISrsiStrategy
{
    Result<IReadOnlyList<SrsiSignal>> EvaluateSignals(IReadOnlyList<Quote> quotes);
}

public interface ISrsiStrategyFactory
{
    ISrsiStrategy GetStrategy(TradingStrategy strategy, Granularity granularity);
}

public class SrsiStrategyFactory : ISrsiStrategyFactory
{
    private readonly IEvaluator _evaluator;

    public SrsiStrategyFactory(IEvaluator evaluator)
    {
        ArgumentNullException.ThrowIfNull(evaluator);
        _evaluator = evaluator;
    }

    public ISrsiStrategy GetStrategy(TradingStrategy strategy, Granularity granularity)
    {
        return (strategy, granularity) switch
        {
            (TradingStrategy.EmaAndStoch, _) => new EmaAndStochStrategy(_evaluator),
            (TradingStrategy.Scalping, _) => new ScalpingStrategy(_evaluator),
            (TradingStrategy.DayTrading, _) => new DailyTradingStrategy(_evaluator),
            (_, Granularity.FiveMins) => new ScalpingStrategy(_evaluator),
            (_, Granularity.Hourly) => new DailyTradingStrategy(_evaluator),
            _ => new EmaAndStochStrategy(_evaluator),
        };
    }
}

public static class SrsiStrategyExtensions
{
    public static bool KDSellSignal(
        SRsiResult last,
        SRsiResult penult,
        SRsiSettings sRsiSettings
    ) =>
        penult.StochK > sRsiSettings.Overbought
        && last.StochK < sRsiSettings.Overbought
        && penult.StochK > penult.StochD
        && last.StochK < last.StochD;

    public static bool KDBuySignal(SRsiResult last, SRsiResult penult, SRsiSettings sRsiSettings) =>
        penult.StochK < sRsiSettings.Oversold
        && last.StochK > sRsiSettings.Oversold
        && penult.StochK < penult.StochD
        && last.StochK > last.StochD;
}

using FluentResults;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;

namespace TradingApp.Module.Quotes.Application.Features.TradeStrategy.WaveTrend;

public interface IWaveTrendStrategy
{
    Result<IReadOnlyList<WaveTrendSignal>> EvaluateSignals(
        IReadOnlyList<Quote> quotes,
        WaveTrendSettings settings,
        Granularity granularity
    );
}

public interface IWaveTrendStrategyFactory
{
    IWaveTrendStrategy GetStrategy(AssetName assetName, Granularity granularity);
}

public class WaveTrendStrategyFactory : IWaveTrendStrategyFactory
{
    private readonly IEvaluator _evaluator;

    public WaveTrendStrategyFactory(IEvaluator evaluator)
    {
        ArgumentNullException.ThrowIfNull(evaluator);
        _evaluator = evaluator;
    }

    public IWaveTrendStrategy GetStrategy(AssetName assetName, Granularity granularity) =>
        (assetName, granularity) switch
        {
            _ => new WaveTrendDefaultStrategy(_evaluator),
        };
}

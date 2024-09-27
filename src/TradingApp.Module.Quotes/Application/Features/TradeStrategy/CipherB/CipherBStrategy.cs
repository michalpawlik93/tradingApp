using FluentResults;
using TradingApp.Domain.Modules.Constants;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.Srsi;
using TradingApp.Module.Quotes.Application.Features.TradeStrategy.WaveTrend;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Constants;

namespace TradingApp.Module.Quotes.Application.Features.TradeStrategy.CipherB;

public interface ICipherBStrategy
{
    Result<(
        IReadOnlyList<MfiResult> mfiResults,
        IReadOnlyList<WaveTrendSignal> waveTrendSignals,
        IReadOnlyList<SrsiSignal> srsiSignals
    )> EvaluateSignals(IReadOnlyList<Quote> quotes, CypherBDecisionSettings settings);
}

public class CipherBStrategy : ICipherBStrategy
{
    private readonly IEvaluator _evaluator;
    private readonly ISrsiStrategyFactory _srsiStrategyFactory;
    private readonly IWaveTrendStrategyFactory _waveTrendStrategyFactory;

    public CipherBStrategy(
        IEvaluator evaluator,
        ISrsiStrategyFactory srsiStrategyFactory,
        IWaveTrendStrategyFactory waveTrendStrategyFactory
    )
    {
        ArgumentNullException.ThrowIfNull(evaluator);
        ArgumentNullException.ThrowIfNull(srsiStrategyFactory);
        ArgumentNullException.ThrowIfNull(waveTrendStrategyFactory);
        _evaluator = evaluator;
        _srsiStrategyFactory = srsiStrategyFactory;
        _waveTrendStrategyFactory = waveTrendStrategyFactory;
    }

    public Result<(
        IReadOnlyList<MfiResult> mfiResults,
        IReadOnlyList<WaveTrendSignal> waveTrendSignals,
        IReadOnlyList<SrsiSignal> srsiSignals
    )> EvaluateSignals(IReadOnlyList<Quote> quotes, CypherBDecisionSettings settings)
    {
        var wtStrategy = _waveTrendStrategyFactory.GetStrategy(
            settings.AssetName,
            settings.Granularity
        );
        var wtSignals = wtStrategy.EvaluateSignals(
            quotes,
            settings.WaveTrendSettings ?? WaveTrendSettingsConst.WaveTrendSettingsDefault,
            settings.Granularity
        );
        if (wtSignals.IsFailed)
        {
            return wtSignals.ToResult();
        }
        var mfiResults = _evaluator.GetMfi(
            quotes,
            settings.MfiSettings ?? MfiSettingsConst.MfiSettingsDefault
        );
        if (mfiResults.Count < 2)
        {
            return Result.Fail("Quotes can not be less than 2 elements");
        }
        var strategy = _srsiStrategyFactory.GetStrategy(
            settings.AssetName,
            settings.Granularity
        );
        var srsiSignals = strategy.EvaluateSignals(quotes);
        if (srsiSignals.IsFailed)
        {
            return srsiSignals.ToResult();
        }
        return Result.Ok((mfiResults, wtSignals.Value, srsiSignals.Value));
    }
}

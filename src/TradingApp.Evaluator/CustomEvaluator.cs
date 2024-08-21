using FluentResults;
using System.Diagnostics.CodeAnalysis;
using TradingApp.Evaluator.Indicators;
using TradingApp.Evaluator.Utils;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Evaluator.Indicators;

namespace TradingApp.Evaluator;


[ExcludeFromCodeCoverage]
public class CustomEvaluator : IEvaluator
{
    private const int DecimalPlace = 4;
    // TODO:All methods should return Result

    public IReadOnlyList<WaveTrendResult> GetWaveTrend(
        IReadOnlyList<Quote> quotes,
        WaveTrendSettings settings
    ) => WaveTrendIndicator.Calculate(quotes, settings, true, DecimalPlace);

    public IReadOnlyList<SRsiResult> GetSrsi(IReadOnlyList<Quote> quotes, SRsiSettings settings) =>
        SRsiIndicator.Calculate(quotes, settings);

    public IReadOnlyList<MfiResult> GetMfi(IReadOnlyList<Quote> quotes, MfiSettings settings) =>
        MoneyFlowIndicator.Calculate(quotes, settings, true, DecimalPlace);

    public IReadOnlyList<RsiResult> GetRsi(IReadOnlyList<Quote> quotes, RsiSettings settings) =>
        RsiIndicator.Calculate(quotes, settings);

    public Result<decimal[]> GetEmea(decimal[] values, int length) =>
     MovingAverage.CalculateEma(length, values);
}

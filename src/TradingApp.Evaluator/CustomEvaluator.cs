using System.Diagnostics.CodeAnalysis;
using TradingApp.Evaluator.Indicators;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Evaluator.Indicators;

namespace TradingApp.Evaluator;

[ExcludeFromCodeCoverage]
public class CustomEvaluator : IEvaluator
{
    private const int DecimalPlace = 4;

    public IEnumerable<WaveTrendResult> GetWaveTrend(IEnumerable<Quote> quotes, WaveTrendSettings settings) =>
        WaveTrendIndicator.Calculate(quotes, settings, true, DecimalPlace);

    public IEnumerable<SRsiResult> GetSrsi(IEnumerable<Quote> quotes, SRsiSettings settings) =>
        SRsiIndicator.Calculate(quotes, settings);

    public IEnumerable<MfiResult> GetMfi(IEnumerable<Quote> quotes, MfiSettings settings) =>
        MoneyFlowIndicator.Calculate(quotes, settings, true, DecimalPlace);

    public IEnumerable<RsiResult> GetRsi(IEnumerable<Quote> quotes, RsiSettings settings) =>
        RsiIndicator.Calculate(quotes, settings);
}

// separate signals from indices data, dont return crossOVers crossDowns. It can be enhancment in a future
// crossDown , buy signals etc can be assesed in decision service
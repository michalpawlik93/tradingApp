using TradingApp.Evaluator.Indicators;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;

namespace TradingApp.Evaluator;

public class CustomEvaluator : IEvaluator
{
    private const int DecimalPlace = 4;
    public ICollection<VWapResult> GetVwap(List<Quote> quotes) =>
        VwapIndicator.Calculate(quotes, DecimalPlace);

    public ICollection<WaveTrendResult> GetWaveTrend(List<Quote> quotes, WaveTrendSettings settings) =>
        WaveTrendIndicator.Calculate(quotes, settings, true, DecimalPlace);

    public ICollection<SRsiResult> GetSRSI(List<Quote> quotes, SRsiSettings settings)
    {
        return SRsiIndicator.Calculate(quotes, settings);
    }

    public ICollection<RsiResult> GetRSI(List<Quote> quotes, RsiSettings settings)
    {
        return RsiIndicator.Calculate(quotes, settings);
    }
}

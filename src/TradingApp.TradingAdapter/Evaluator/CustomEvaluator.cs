using TradingApp.TradingAdapter.Indicators;
using TradingApp.TradingAdapter.Interfaces;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.TradingAdapter.Evaluator;

public class CustomEvaluator : IEvaluator
{
    private const int DecimalPlace = 4;
    public ICollection<VWap> GetVwap(List<Quote> quotes) =>
        VwapIndicator.CalculateVWAP(quotes, DecimalPlace);

    public ICollection<WaveTrend> GetWaveTrend(List<Quote> quotes, WaveTrendSettings settings) =>
        WaveTrendIndicator.GetWaveTrend(quotes, settings, true, DecimalPlace);

    public ICollection<SRsi> GetSRSI(List<Quote> quotes, SRsiSettings settings)
    {
        return SRsiIndicator.Calculate(quotes, settings);
    }

    public ICollection<Rsi> GetRSI(List<Quote> quotes, RsiSettings settings)
    {
        return RsiIndicator.Calculate(quotes, settings);
    }
}

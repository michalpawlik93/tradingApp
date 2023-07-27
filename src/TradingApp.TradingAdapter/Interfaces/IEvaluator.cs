using TradingApp.TradingAdapter.Models;

namespace TradingApp.TradingAdapter.Interfaces;

public interface IEvaluator
{
    ICollection<Rsi> GetRSI(
        List<Quote> quotes,
        RsiSettings settings
    );
    ICollection<VWap> GetVwap(List<Quote> quotes);
    ICollection<WaveTrend> GetWaveTrend(
        List<Quote> quotes,
        WaveTrendSettings settings
    );
    ICollection<SRsi> GetSRSI(
        List<Quote> quotes,
        SRsiSettings settings
    );
}

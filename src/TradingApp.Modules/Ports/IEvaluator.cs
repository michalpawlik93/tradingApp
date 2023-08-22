using TradingApp.Modules.Application.Models;

namespace TradingApp.Modules.Ports;

public interface IEvaluator
{
    ICollection<RsiResult> GetRSI(
        List<Quote> quotes,
        RsiSettings settings
    );
    ICollection<VWapResult> GetVwap(List<Quote> quotes);
    ICollection<WaveTrendResult> GetWaveTrend(
        List<Quote> quotes,
        WaveTrendSettings settings
    );
    ICollection<SRsiResult> GetSRSI(
        List<Quote> quotes,
        SRsiSettings settings
    );
}

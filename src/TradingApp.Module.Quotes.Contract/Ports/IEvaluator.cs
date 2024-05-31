using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Contract.Ports;


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
    ICollection<MfiResult> GetMfi(
        List<Quote> quotes,
        MfiSettings settings
    );
}

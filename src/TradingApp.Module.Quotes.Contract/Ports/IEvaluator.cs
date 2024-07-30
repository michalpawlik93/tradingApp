using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Contract.Ports;


public interface IEvaluator
{
    IEnumerable<RsiResult> GetRsi(
        IEnumerable<Quote> quotes,
        RsiSettings settings
    );
    IEnumerable<WaveTrendResult> GetWaveTrend(
        IEnumerable<Quote> quotes,
        WaveTrendSettings settings
    );
    IReadOnlyList<SRsiResult> GetSrsi(
        IReadOnlyList<Quote> quotes,
        SRsiSettings settings
    );
    IEnumerable<MfiResult> GetMfi(
        IEnumerable<Quote> quotes,
        MfiSettings settings
    );
}

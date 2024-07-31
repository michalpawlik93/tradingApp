using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Contract.Ports;


public interface IEvaluator
{
    IEnumerable<RsiResult> GetRsi(
        IEnumerable<Quote> quotes,
        RsiSettings settings
    );
    IReadOnlyList<WaveTrendResult> GetWaveTrend(
        IReadOnlyList<Quote> quotes,
        WaveTrendSettings settings
    );
    IReadOnlyList<SRsiResult> GetSrsi(
        IReadOnlyList<Quote> quotes,
        SRsiSettings settings
    );
    IReadOnlyList<MfiResult> GetMfi(
        IReadOnlyList<Quote> quotes,
        MfiSettings settings
    );
}

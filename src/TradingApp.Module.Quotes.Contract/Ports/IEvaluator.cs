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
    IEnumerable<SRsiResult> GetSrsi(
        IEnumerable<Quote> quotes,
        SRsiSettings settings
    );
    IEnumerable<MfiResult> GetMfi(
        IEnumerable<Quote> quotes,
        MfiSettings settings
    );
}

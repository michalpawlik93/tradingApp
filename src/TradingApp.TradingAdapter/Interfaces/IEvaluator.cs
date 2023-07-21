using TradingApp.TradingAdapter.Constants;
using TradingApp.TradingAdapter.Models;
using DomainQuote = TradingApp.TradingAdapter.Models.Quote;

namespace TradingApp.TradingAdapter.Interfaces;

public interface IEvaluator
{
    IEnumerable<decimal?> GetRSI(
        IEnumerable<DomainQuote> domainQuotes,
        int lookBackPeriod = RsiSettingsConst.DefaultPeriod
    );
    IEnumerable<decimal?> GetMFI(
        IEnumerable<DomainQuote> domainQuotes,
        int lookBackPeriod = RsiSettingsConst.DefaultPeriod
    );
    IEnumerable<decimal?> GetVwap(IEnumerable<DomainQuote> domainQuotes);
    IEnumerable<decimal?> GetMomentumWave(
        IEnumerable<DomainQuote> domainQuotes,
        int lookBackPeriod = RsiSettingsConst.DefaultPeriod
    );
    IEnumerable<WaveTrend> GetWaveTrend(IEnumerable<DomainQuote> domainQuotes, WaveTrendSettings settings);
}

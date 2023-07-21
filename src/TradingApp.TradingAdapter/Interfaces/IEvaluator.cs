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
    IEnumerable<decimal?> GetVwap(List<DomainQuote> domainQuotes);
    IEnumerable<WaveTrend> GetWaveTrend(IEnumerable<DomainQuote> domainQuotes, WaveTrendSettings settings);
}

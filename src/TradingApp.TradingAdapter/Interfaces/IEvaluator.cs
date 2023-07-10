using TradingApp.TradingAdapter.Constants;
using TradingApp.TradingAdapter.Models;
using DomainQuote = TradingApp.TradingAdapter.Models.Quote;

namespace TradingApp.TradingAdapter.Interfaces;

public interface IEvaluator
{
    IEnumerable<decimal?> GetRSI(
        IEnumerable<DomainQuote> domeinQuotes,
        int loockBackPeriod = RsiSettingsConst.DefaultPeriod
    );
    IEnumerable<decimal?> GetMFI(
        IEnumerable<DomainQuote> domeinQuotes,
        int loockBackPeriod = RsiSettingsConst.DefaultPeriod
    );
    IEnumerable<decimal?> GetVwap(IEnumerable<DomainQuote> domeinQuotes);
    IEnumerable<decimal?> GetMomentumWave(
        IEnumerable<DomainQuote> domeinQuotes,
        int loockBackPeriod = RsiSettingsConst.DefaultPeriod
    );
    IEnumerable<WaveTrend> GetWaveTrend(IEnumerable<DomainQuote> domeinQuotes);
}

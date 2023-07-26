using Skender.Stock.Indicators;
using TradingApp.Common.Utilities;
using TradingApp.TradingAdapter.Constants;
using TradingApp.TradingAdapter.Interfaces;
using TradingApp.TradingAdapter.Mappers;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.TradingAdapter.Evaluator;

public interface ISkenderEvaluator : IEvaluator { }

public class SkenderEvaluator : ISkenderEvaluator
{
    public IEnumerable<decimal?> GetRSI(
        IEnumerable<DomainQuote> domainQuotes,
        int lookBackPeriod = RsiSettingsConst.DefaultPeriod
    ) =>
        domainQuotes
            .MapToSkenderQuotes()
            .GetRsi(lookBackPeriod)
            .Select(r => r.Rsi.ToNullableDecimal());

    public IEnumerable<decimal?> GetMFI(
        IEnumerable<DomainQuote> domainQuotes,
        int lookBackPeriod = 14
    ) =>
        domainQuotes
            .MapToSkenderQuotes()
            .GetMfi(lookBackPeriod)
            .Select(r => r.Mfi.ToNullableDecimal());

    public IEnumerable<decimal?> GetVwap(List<DomainQuote> domainQuotes) =>
        domainQuotes.MapToSkenderQuotes().GetVwap().Select(r => r.Vwap.ToNullableDecimal());

    public IEnumerable<WaveTrend> GetWaveTrend(IEnumerable<DomainQuote> domainQuotes, WaveTrendSettings settings)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Srsi> GetSRSI(IEnumerable<DomainQuote> domainQuotes, SRsiSettings settings)
    {
        throw new NotImplementedException();
    }
}
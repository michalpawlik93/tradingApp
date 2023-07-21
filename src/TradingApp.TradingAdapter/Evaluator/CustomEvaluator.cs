using Skender.Stock.Indicators;
using TradingApp.Common.Utilities;
using TradingApp.TradingAdapter.Constants;
using TradingApp.TradingAdapter.CustomIndexes;
using TradingApp.TradingAdapter.Interfaces;
using TradingApp.TradingAdapter.Mappers;
using TradingApp.TradingAdapter.Models;
using DomainQuote = TradingApp.TradingAdapter.Models.Quote;

namespace TradingApp.TradingAdapter.Evaluator;

public interface ICustomEvaluator : IEvaluator { }

public class CustomEvaluator : ICustomEvaluator
{
    public IEnumerable<decimal?> GetRSI(
        IEnumerable<DomainQuote> domainQuotes,
        int lookBackPeriod = RsiSettingsConst.DefaultPeriod
    ) => throw new NotImplementedException();

    public IEnumerable<decimal?> GetMFI(
        IEnumerable<DomainQuote> domainQuotes,
        int lookBackPeriod = 14
    ) =>
        domainQuotes
            .MapToSkenderQuotes()
            .GetMfi(lookBackPeriod)
            .Select(r => r.Mfi.ToNullableDecimal());

    public IEnumerable<decimal?> GetVwap(List<DomainQuote> domainQuotes) =>
        VwapCustom.CalculateVWAP(domainQuotes);

    public IEnumerable<WaveTrend> GetWaveTrend(IEnumerable<DomainQuote> domainQuotes, WaveTrendSettings settings)
    {
        return WaveTrendProrealCode.GetWaveTrend(domainQuotes, settings);
    }
}

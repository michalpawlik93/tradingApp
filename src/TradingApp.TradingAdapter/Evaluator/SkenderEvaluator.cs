using Skender.Stock.Indicators;
using TradingApp.Common.Utilities;
using TradingApp.TradingAdapter.Constants;
using TradingApp.TradingAdapter.Interfaces;
using TradingApp.TradingAdapter.Mappers;
using TradingApp.TradingAdapter.Models;
using DomainQuote = TradingApp.TradingAdapter.Models.Quote;

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

    public IEnumerable<decimal?> GetVwap(IEnumerable<DomainQuote> domainQuotes) =>
        domainQuotes.MapToSkenderQuotes().GetVwap().Select(r => r.Vwap.ToNullableDecimal());

    public IEnumerable<decimal?> GetMomentumWave(
        IEnumerable<DomainQuote> domainQuotes,
        int lookBackPeriod = RsiSettingsConst.DefaultPeriod
    )
    {
        RsiResult[] rsiResults = domainQuotes.MapToSkenderQuotes().GetRsi(lookBackPeriod).ToArray();
        RocResult[] rocResults = domainQuotes.MapToSkenderQuotes().GetRoc(lookBackPeriod).ToArray();

        return domainQuotes.Select(
            (_, i) =>
            {
                if (rsiResults[i]?.Rsi is null || rocResults[i]?.Roc is null)
                    return null;

                decimal? momentumWave =
                    (Convert.ToDecimal(rsiResults[i].Rsi) + Convert.ToDecimal(rocResults[i].Roc))
                    / 2;
                return momentumWave;
            }
        );
    }

    public IEnumerable<WaveTrend> GetWaveTrend(IEnumerable<DomainQuote> domainQuotes, WaveTrendSettings settings)
    {
        throw new NotImplementedException();
    }
}
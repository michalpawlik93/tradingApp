using Skender.Stock.Indicators;
using TradingApp.TradingAdapter.Constants;
using TradingApp.TradingAdapter.Interfaces;
using TradingApp.TradingAdapter.Models;
using DomainQuote = TradingApp.TradingAdapter.Models.Quote;
using Quote = Skender.Stock.Indicators.Quote;

namespace TradingApp.TradingAdapter.Evaluator;

public interface ISkenderEvaluator : IEvaluator { }

public class SkenderEvaluator : ISkenderEvaluator
{
    public IEnumerable<decimal?> GetRSI(
        IEnumerable<DomainQuote> domeinQuotes,
        int loockBackPeriod = RsiSettingsConst.DefaultPeriod
    ) =>
        domeinQuotes
            .MapToSkenderQuotes()
            .GetRsi(loockBackPeriod)
            .Select(r => r.Rsi.ToNullableDecimal());

    public IEnumerable<decimal?> GetMFI(
        IEnumerable<DomainQuote> domeinQuotes,
        int loockBackPeriod = 14
    ) =>
        domeinQuotes
            .MapToSkenderQuotes()
            .GetMfi(loockBackPeriod)
            .Select(r => r.Mfi.ToNullableDecimal());

    public IEnumerable<decimal?> GetVwap(IEnumerable<DomainQuote> domeinQuotes) =>
        domeinQuotes.MapToSkenderQuotes().GetVwap().Select(r => r.Vwap.ToNullableDecimal());

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

    public IEnumerable<WaveTrend> GetWaveTrend(IEnumerable<DomainQuote> domeinQuotes)
    {
        throw new NotImplementedException();
    }
}

public static class Extensions
{
    public static IEnumerable<Quote> MapToSkenderQuotes(
        this IEnumerable<DomainQuote> domeinQuotes
    ) =>
        domeinQuotes.Select(
            q =>
                new Quote()
                {
                    Open = q.Open,
                    Close = q.Close,
                    High = q.High,
                    Low = q.Low,
                    Date = q.Date,
                    Volume = q.Volume
                }
        );

    public static decimal? ToNullableDecimal(this double? input) =>
        input.HasValue ? (decimal?)input.Value : null;
}

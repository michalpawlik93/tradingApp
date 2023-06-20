using Skender.Stock.Indicators;
using TradingApp.TradingAdapter.Constants;
using DomainQuote = TradingApp.TradingAdapter.Models.Quote;
using Quote = Skender.Stock.Indicators.Quote;

namespace TradingApp.TradingAdapter.Evaluator;

public interface ISkenderEvaluator
{
    IEnumerable<double?> GetRSI(
        IEnumerable<DomainQuote> domeinQuotes,
        int loockBackPeriod = RsiSettingsConst.DefaultPeriod
    );
    IEnumerable<double?> GetMFI(
        IEnumerable<DomainQuote> domeinQuotes,
        int loockBackPeriod = RsiSettingsConst.DefaultPeriod
    );
    IEnumerable<double?> GetVwap(IEnumerable<DomainQuote> domeinQuotes);
    IEnumerable<double?> GetMomentumWave(
        IEnumerable<DomainQuote> domeinQuotes,
        int loockBackPeriod = RsiSettingsConst.DefaultPeriod
    );
}

public class SkenderEvaluator : ISkenderEvaluator
{
    public IEnumerable<double?> GetRSI(
        IEnumerable<DomainQuote> domeinQuotes,
        int loockBackPeriod = RsiSettingsConst.DefaultPeriod
    ) => domeinQuotes.MapToSkenderQuotes().GetRsi(loockBackPeriod).Select(r => r.Rsi);

    public IEnumerable<double?> GetMFI(
        IEnumerable<DomainQuote> domeinQuotes,
        int loockBackPeriod = 14
    ) => domeinQuotes.MapToSkenderQuotes().GetMfi(loockBackPeriod).Select(r => r.Mfi);

    public IEnumerable<double?> GetVwap(IEnumerable<DomainQuote> domeinQuotes) =>
        domeinQuotes.MapToSkenderQuotes().GetVwap().Select(r => r.Vwap);

    public IEnumerable<double?> GetMomentumWave(
        IEnumerable<DomainQuote> domeinQuotes,
        int loockBackPeriod = RsiSettingsConst.DefaultPeriod
    )
    {
        RsiResult[] rsiResults = domeinQuotes
            .MapToSkenderQuotes()
            .GetRsi(loockBackPeriod)
            .ToArray();
        RocResult[] rocResults = domeinQuotes
            .MapToSkenderQuotes()
            .GetRoc(loockBackPeriod)
            .ToArray();

        return domeinQuotes.Select(
            (_, i) =>
                rsiResults[i]?.Rsi is null || rocResults[i]?.Roc is null
                    ? null
                    : (rsiResults[i].Rsi + rocResults[i].Roc) / 2
        );
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
}

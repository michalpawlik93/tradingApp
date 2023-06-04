using Skender.Stock.Indicators;
using TradingApp.TradingAdapter.Constants;
using DomainQuote = TradingApp.TradingAdapter.Models.Quote;
using Quote = Skender.Stock.Indicators.Quote;

namespace TradingApp.TradingAdapter.Evaluator;

public interface ISkenderEvaluator
{
    IEnumerable<double?> GetSMA(IEnumerable<DomainQuote> domeinQuotes);
    IEnumerable<double?> GetRSI(IEnumerable<DomainQuote> domeinQuotes, int loockBackPeriod = RsiSettingsConst.DefaultPeriod);
}
public class SkenderEvaluator : ISkenderEvaluator
{
    public IEnumerable<double?> GetSMA(IEnumerable<DomainQuote> domeinQuotes)
    {
        var quotes = domeinQuotes.Select(q => new Quote()
        {
            Open = q.Open,
            Close = q.Close,
            High = q.High,
            Low = q.Low,
            Date = q.Date,
            Volume = q.Volume
        });
        IEnumerable<SmaResult> results = quotes.GetSma(20);
        return results.Select(r => r.Sma);
    }

    public IEnumerable<double?> GetRSI(IEnumerable<DomainQuote> domeinQuotes, int loockBackPeriod = RsiSettingsConst.DefaultPeriod)
    {
        var quotes = domeinQuotes.Select(q => new Quote()
        {
            Open = q.Open,
            Close = q.Close,
            High = q.High,
            Low = q.Low,
            Date = q.Date,
            Volume = q.Volume
        });
        IEnumerable<RsiResult> results = quotes.GetRsi(loockBackPeriod);
        return results.Select(r => r.Rsi);
    }
}

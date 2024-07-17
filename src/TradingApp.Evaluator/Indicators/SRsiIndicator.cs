using TradingApp.Evaluator.Utils;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Evaluator.Indicators;

public static class SRsiIndicator
{
    public static IEnumerable<SRsiResult> Calculate(IEnumerable<Quote> domainQuotes, SRsiSettings settings)
    {
        var rsiPeriods = settings.ChannelLength;
        var stochPeriods = settings.ChannelLength;
        var smoothPeriodsD = settings.StochDSmooth;
        var smoothPeriodsK = settings.StochKSmooth;

        var length = domainQuotes.Count();
        var initPeriods = Math.Min(rsiPeriods + stochPeriods - 1, length);
        List<SRsiResult> results = new(length);

        for (var i = 0; i < initPeriods; i++)
        {
            results.Add(new SRsiResult(domainQuotes.ElementAt(i).Date, null, null));
        }
        var rsiResults = RsiIndicator.Calculate(domainQuotes, new RsiSettings(settings.Oversold, settings.Overbought, true, settings.ChannelLength))
            .Remove(Math.Min(rsiPeriods, length)).
            Select((x, index) => new Quote
            {
                Date = domainQuotes.ElementAt(index).Date,
                High = x.Value ?? 0,
                Low = x.Value ?? 0,
                Close = x.Value ?? 0,
            }).ToList();

        var stoResults =
            rsiResults
            .Calculate(
                stochPeriods,
                smoothPeriodsD,
                smoothPeriodsK, 3, 2, MaType.SMA)
            .ToList();


        for (var i = rsiPeriods + stochPeriods - 1; i < length; i++)
        {
            var r = stoResults[i - rsiPeriods];
            results.Add(new SRsiResult(r.Date, r.Oscillator, r.Signal));
        }

        return results;
    }
}

//https://pl.tradingview.com/script/T85iFvQj-VuManChu-Cipher-B-Divergences-Strategy/
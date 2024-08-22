using TradingApp.Evaluator.Utils;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Evaluator.Indicators;

public static class SRsiIndicator
{
    public static IReadOnlyList<SRsiResult> Calculate(
        IReadOnlyList<Quote> domainQuotes,
        SrsiSettings settings
    )
    {
        var length = domainQuotes.Count;
        var initPeriods = Math.Min(settings.ChannelLength + settings.ChannelLength - 1, length);
        List<SRsiResult> results = new(length);

        for (var i = 0; i < initPeriods; i++)
        {
            results.Add(new SRsiResult(domainQuotes[i].Date, null, null));
        }
        var rsiResults = RsiIndicator
            .Calculate(
                domainQuotes,
                new RsiSettings(
                    settings.Oversold,
                    settings.Overbought,
                    true,
                    settings.ChannelLength
                )
            )
            .Remove(Math.Min(settings.ChannelLength, length))
            .Select(
                (x, index) =>
                    new Quote
                    {
                        Date = domainQuotes[index].Date,
                        High = x.Value ?? 0,
                        Low = x.Value ?? 0,
                        Close = x.Value ?? 0,
                    }
            )
            .ToList();

        var stoResults = rsiResults.Calculate(
            settings.ChannelLength,
            settings.StochDSmooth,
            settings.StochKSmooth,
            3,
            2,
            MaType.SMA
        );

        for (var i = settings.ChannelLength + settings.ChannelLength - 1; i < length; i++)
        {
            var r = stoResults[i - settings.ChannelLength];
            results.Add(new SRsiResult(r.Date, r.Oscillator, r.Signal));
        }

        return results;
    }
}

//https://pl.tradingview.com/script/T85iFvQj-VuManChu-Cipher-B-Divergences-Strategy/

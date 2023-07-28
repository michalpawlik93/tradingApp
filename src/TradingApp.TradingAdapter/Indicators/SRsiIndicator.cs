using TradingApp.TradingAdapter.Enums;
using TradingApp.TradingAdapter.Models;
using TradingApp.TradingAdapter.Utils;

namespace TradingApp.TradingAdapter.Indicators
{
    /// <summary>
    /// D - Signal
    /// K - Oscilator
    /// </summary>
    public static class SRsiIndicator
    {
        public static List<SRsiResult> Calculate(List<Quote> domainQuotes, SRsiSettings settings)
        {
            var rsiPeriods = settings.Length;
            var stochPeriods = settings.Length;
            var smoothPeriodsD = settings.StochDSmooth;
            var smoothPeriodsK = settings.StochKSmooth;
            // check parameter arguments
            ValidateStochRsi(rsiPeriods, stochPeriods, smoothPeriodsK, smoothPeriodsD);

            // initialize results
            int length = domainQuotes.Count;
            int initPeriods = Math.Min(rsiPeriods + stochPeriods - 1, length);
            List<SRsiResult> results = new(length);

            for (int i = 0; i < initPeriods; i++)
            {
                results.Add(new SRsiResult(domainQuotes[i].Date, null, null));
            }
            var rsiResults = RsiIndicator.Calculate(domainQuotes, new RsiSettings(settings.Oversold, settings.Overbought, true, settings.Length))
                .Remove(Math.Min(rsiPeriods, length)).
                Select((x, index) => new Quote
                {
                    Date = domainQuotes[index].Date,
                    High = x.Value ?? 0,
                    Low = x.Value ?? 0,
                    Close = x.Value ?? 0,
                }).ToList();

            List<StochResult> stoResults =
                rsiResults
                .Calculate(
                    stochPeriods,
                    smoothPeriodsD,
                    smoothPeriodsK, 3, 2, MaType.SMA)
                .ToList();


            for (int i = rsiPeriods + stochPeriods - 1; i < length; i++)
            {
                StochResult r = stoResults[i - rsiPeriods];
                results.Add(new SRsiResult(r.Date, r.Oscillator, r.Signal));
            }

            return results;
        }

        private static void ValidateStochRsi(
            int stochPeriods,
            int signalPeriods,
            int smoothPeriodsK,
            int smoothPeriodsD)
        {

            if (stochPeriods <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(stochPeriods), stochPeriods,
                    "STOCH periods must be greater than 0 for Stochastic RSI.");
            }

            if (signalPeriods <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(signalPeriods), signalPeriods,
                    "Signal periods must be greater than 0 for Stochastic RSI.");
            }

            if (smoothPeriodsK <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(smoothPeriodsK), smoothPeriodsK,
                    "Smooth periods must be greater than 0 for Stochastic RSI.");
            }

            if (smoothPeriodsD <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(smoothPeriodsD), smoothPeriodsD,
                    "Smooth periods must be greater than 0 for Stochastic RSI.");
            }
        }
    }
}

//https://pl.tradingview.com/script/T85iFvQj-VuManChu-Cipher-B-Divergences-Strategy/
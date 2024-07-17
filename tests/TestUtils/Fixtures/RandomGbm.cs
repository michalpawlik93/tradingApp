using TradingApp.Module.Quotes.Contract.Models;

namespace TestUtils.Fixtures;

/**
<summary>
Geometric Brownian Motion (GMB) is a random simulator of market movement.
GBM can be used for testing indicators, validation and Monte Carlo simulations of strategies.

Sample usage:
RandomGbm data = new(); // generates 1 year (252) list of bars
RandomGbm data = new(Bars: 1000); // generates 1,000 bars
RandomGbm data = new(Bars: 252, Volatility: 0.05, Drift: 0.0005, Seed: 100.0)

Parameters
Bars:       number of bars (quotes) requested
Volatility: how dymamic/volatile the series should be; default is 1
Drift:      incremental drift due to annual interest rate; default is 5%, allows inclusion of long-term 
Seed:       starting value of the random series; should not be 0.
</summary>
**/
public class RandomGbm : List<Quote>
{
    private readonly double _volatility;
    private readonly double _drift;
    private double _seed;

    public RandomGbm(
        int bars = 250,
        double volatility = 1.0,
        double drift = 0.05,
        double seed = 10000000.0
    )
    {
        _seed = seed;
        _volatility = volatility * 0.01;
        _drift = drift * 0.01;
        for (var i = 0; i < bars; i++)
        {
            var date = DateTime.Today.AddDays(i - bars);
            Add(date);
        }
    }

    public void Add(DateTime timestamp)
    {
        var open = Price(_seed, _volatility * _volatility, _drift);
        var close = Price(open, _volatility, _drift);

        var ocMax = Math.Max(open, close);
        var high = Price(_seed, _volatility * 0.5, 0);
        high = (high < ocMax) ? (2 * ocMax) - high : high;

        var ocMin = Math.Min(open, close);
        var low = Price(_seed, _volatility * 0.5, 0);
        low = (low > ocMin) ? (2 * ocMin) - low : low;

        var volume = Price(_seed * 10, _volatility * 2, drift: 0);

        Quote quote =
            new()
            {
                Date = timestamp,
                Open = (decimal)open,
                High = (decimal)high,
                Low = (decimal)low,
                Close = (decimal)close,
                Volume = (decimal)volume
            };

        Add(quote);
        _seed = close;
    }

    private static double Price(double seed, double volatility, double drift)
    {
        Random rnd = new((int)DateTime.UtcNow.Ticks);
        var u1 = 1.0 - rnd.NextDouble();
        var u2 = 1.0 - rnd.NextDouble();
        var z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
        return seed * Math.Exp(drift - (volatility * volatility * 0.5) + (volatility * z));
    }
}

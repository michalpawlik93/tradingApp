using System.Diagnostics.CodeAnalysis;

namespace TradingApp.Module.Quotes.Contract.Models;

[ExcludeFromCodeCoverage]
public record StochResult
{
    public StochResult(DateTime date)
    {
        Date = date;
    }
    /// <summary>
    /// Percent %J, the second element of the Stochastic RSI indicator.
    /// </summary>
    public decimal? PercentJ { get; set; }

    /// <summary>
    /// Stoch K, the main indicator of Stochastic RSI.
    /// Represents the fast oscillator line.
    /// </summary>
    public decimal? Oscillator { get; set; }

    /// <summary>
    /// Stoch D, the signal line of the Stochastic RSI indicator.
    /// Helps identify trends by smoothing out the faster changes in Stoch K.
    /// </summary>
    public decimal? Signal { get; set; }
    public DateTime Date { get; set; }
}

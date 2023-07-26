namespace TradingApp.Application.Models;

/// <summary>
/// Data transfer object for configuring oscillation parameters.
/// </summary>
public class OscillationSettings
{
    /// <summary>
    /// Gets or sets the threshold level below which an asset is considered oversold.
    /// </summary>
    public double Oversold { get; set; }

    /// <summary>
    /// Gets or sets the threshold level above which an asset is considered overbought.
    /// </summary>
    public double Overbought { get; set; }
}

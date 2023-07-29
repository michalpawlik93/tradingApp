namespace TradingApp.Modules.Models;

/// <summary>
/// Data transfer object for configuring SRSI indicator parameters.
/// </summary>
public class SRsiSettingsDto : OscillationSettings
{
    /// <summary>
    /// Show SRSI
    /// </summary>
    public bool Enable { get; set; }

    /// <summary>
    /// Gets or sets the number of periods used to calculate the SRSI width.
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Gets or sets the number of periods used to calculate the Stochastic RSI %K value.
    /// </summary>
    /// Example value: 3
    public int StochKSmooth { get; set; }

    /// <summary>
    /// Gets or sets the number of periods used to calculate the Stochastic RSI %D value.
    /// </summary>
    /// Example value: 3
    public int StochDSmooth { get; set; }
};

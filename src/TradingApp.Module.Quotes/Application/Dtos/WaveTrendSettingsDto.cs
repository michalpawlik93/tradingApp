using System.ComponentModel;
using TradingApp.Module.Quotes.Application.Models;

namespace TradingApp.Module.Quotes.Application.Dtos;

/// <summary>
/// Data transfer object for configuring WaveTrend indicator parameters.
/// </summary>
public class WaveTrendSettingsDto : OscillationSettings
{
    /// <summary>
    /// Gets or sets the number of periods used to calculate the channel's width.
    /// </summary>
    [DefaultValue(8)]
    public int ChannelLength { get; set; }

    /// <summary>
    /// Gets or sets the number of periods used to compute the average true range.
    /// </summary>
    [DefaultValue(8)]
    public int AverageLength { get; set; }

    /// <summary>
    /// Gets or sets the number of periods for the moving average, which smoothens the trend.
    /// </summary>
    [DefaultValue(3)]
    public int MovingAverageLength { get; set; }

    /// <summary>
    /// Show Vwap for WaveTrend
    /// </summary>
    [DefaultValue(true)]
    public decimal EnableVwap { get; set; }

    /// <summary>
    /// Show WaveTrend
    /// </summary>
    [DefaultValue(true)]
    public decimal Enable { get; set; }
}

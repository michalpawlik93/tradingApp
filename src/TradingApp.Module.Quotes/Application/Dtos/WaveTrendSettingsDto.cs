using System.ComponentModel;
using TradingApp.Domain.Modules.Constants;
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
    [DefaultValue(WaveTrendSettingsConst.ChannelLength)]
    public int ChannelLength { get; set; }

    /// <summary>
    /// Gets or sets the number of periods used to compute the average true range.
    /// </summary>
    [DefaultValue(WaveTrendSettingsConst.AverageLength)]
    public int AverageLength { get; set; }

    /// <summary>
    /// Gets or sets the number of periods for the moving average, which smoothens the trend.
    /// </summary>
    [DefaultValue(WaveTrendSettingsConst.MovingAverageLength)]
    public int MovingAverageLength { get; set; }

    /// <summary>
    /// Show Vwap for WaveTrend
    /// </summary>
    [DefaultValue(true)]
    public bool EnableVwap { get; set; }

    /// <summary>
    /// Show WaveTrend
    /// </summary>
    [DefaultValue(true)]
    public bool Enable { get; set; }

    /// <summary>
    /// Gets or sets the threshold level below which an asset is considered oversold.
    [DefaultValue(WaveTrendSettingsConst.OversoldLevel2)]
    public decimal OversoldLevel2 { get; set; }

    /// <summary>
    /// Gets or sets the threshold level above which an asset is considered overbought.
    /// </summary>
    [DefaultValue(WaveTrendSettingsConst.OverboughtLevel2)]
    public decimal OverboughtLevel2 { get; set; }
}

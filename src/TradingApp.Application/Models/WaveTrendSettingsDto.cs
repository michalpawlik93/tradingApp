namespace TradingApp.Application.Models;

/// <summary>
/// Data transfer object for configuring WaveTrend indicator parameters.
/// </summary>
public class WaveTrendSettingsDto
{
    /// <summary>
    /// Gets or sets the number of periods used to calculate the channel's width.
    /// </summary>
    public int ChannelLength { get; set; }

    /// <summary>
    /// Gets or sets the number of periods used to compute the average true range.
    /// </summary>
    public int AverageLength { get; set; }

    /// <summary>
    /// Gets or sets the number of periods for the moving average, which smoothens the trend.
    /// </summary>
    public int MovingAverageLength { get; set; }

    /// <summary>
    /// Gets or sets the threshold level below which an asset is considered oversold.
    /// </summary>
    public decimal Oversold { get; set; }

    /// <summary>
    /// Gets or sets the threshold level above which an asset is considered overbought.
    /// </summary>
    public decimal Overbought { get; set; }
}

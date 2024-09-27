using System.ComponentModel;
using TradingApp.Module.Quotes.Application.Models;

namespace TradingApp.Module.Quotes.Application.Dtos;

/// <summary>
/// Data transfer object for configuring RSI indicator parameters.
/// </summary>
public class RsiSettingsDto : OscillationSettings
{
    /// <summary>
    /// Show RSI
    /// </summary>
    [DefaultValue(true)]
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the number of periods used to calculate the RSI width.
    /// </summary>
    [DefaultValue(8)]
    public int ChannelLength { get; set; }
};
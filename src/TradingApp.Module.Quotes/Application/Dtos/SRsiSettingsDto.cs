﻿using System.ComponentModel;
using TradingApp.Module.Quotes.Application.Models;

namespace TradingApp.Module.Quotes.Application.Dtos;

/// <summary>
/// Data transfer object for configuring SRSI indicator parameters.
/// </summary>
public class SrsiSettingsDto : OscillationSettings
{
    /// <summary>
    /// Show SRSI
    /// </summary>
    [DefaultValue(true)]
    public bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the number of periods used to calculate the SRSI width.
    /// </summary>
    [DefaultValue(8)]
    public int ChannelLength { get; set; }

    /// <summary>
    /// Gets or sets the number of periods used to calculate the Stochastic RSI %K value.
    /// </summary>
    [DefaultValue(3)]
    public int StochKSmooth { get; set; }

    /// <summary>
    /// Gets or sets the number of periods used to calculate the Stochastic RSI %D value.
    /// </summary>
    [DefaultValue(3)]
    public int StochDSmooth { get; set; }
};

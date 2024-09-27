using System.ComponentModel;
using TradingApp.Module.Quotes.Domain.Constants;

namespace TradingApp.Module.Quotes.Application.Dtos;

public class MfiSettingsDto
{
    /// <summary>
    /// Gets or sets the number of periods used to calculate the channel's width.
    /// </summary>
    [DefaultValue(MfiSettingsConst.ChannelLength)]
    public int ChannelLength { get; set; }

    /// <summary>
    /// Gets or sets scale factor.
    /// </summary>
    [DefaultValue(MfiSettingsConst.ScaleFactor)]
    public int ScaleFactor { get; set; }
}


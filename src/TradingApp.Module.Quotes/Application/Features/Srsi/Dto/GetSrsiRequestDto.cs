using System.ComponentModel;
using TradingApp.Module.Quotes.Application.Dtos;

namespace TradingApp.Module.Quotes.Application.Features.Srsi.Dto;

/// <summary>
/// Data transfer object for fetching Srsi data.
/// </summary>
public class GetSrsiRequestDto
{
    /// <summary>
    /// Gets or sets the settings for the Asset.
    /// </summary>
    public AssetDto Asset { get; set; }


    /// <summary>
    /// Gets or sets the settings for the TimeFrame.
    /// </summary>
    public TimeFrameDto TimeFrame { get; set; }

    /// <summary>
    /// Gets or sets the settings for the SRSI calculation.
    /// </summary>
    public SRsiSettingsDto SRsiSettings { get; set; }

    /// <summary>
    ///  Trading Strategy, chose one appropriate for granularity
    /// </summary>
    /// <example>Scalping</example>
    [DefaultValue(nameof(TradeStrategy.TradingStrategy.Scalping))]
    public string TradingStrategy { get; set; }
}


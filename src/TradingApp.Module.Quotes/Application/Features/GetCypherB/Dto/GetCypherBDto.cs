using System.ComponentModel;
using TradingApp.Module.Quotes.Application.Dtos;

namespace TradingApp.Module.Quotes.Application.Features.GetCypherB.Dto;

/// <summary>
/// Data transfer object for fetching OHLC data.
/// </summary>
public class GetCypherBDto
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
    /// Gets or sets the settings for the WaveTrend calculation.
    /// </summary>
    public WaveTrendSettingsDto WaveTrendSettings { get; set; }

    /// <summary>
    /// Gets or sets the settings for the SRSI calculation.
    /// </summary>
    public SRsiSettingsDto SRsiSettings { get; set; }

    /// <summary>
    /// Gets or sets the settings for the Mfi calculation.
    /// </summary>
    public MfiSettingsDto MfiSettings { get; set; }

    /// <summary>
    ///  Trading Strategy, chose one appropriate for granularity
    /// </summary>
    /// <example>Scalping</example>
    [DefaultValue(nameof(TradeStrategy.TradingStrategy.Scalping))]
    public string TradingStrategy { get; set; }
}

using TradingApp.Modules.Application.Dtos;

namespace TradingApp.Modules.Application.GetCypherB.Dto;

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
}

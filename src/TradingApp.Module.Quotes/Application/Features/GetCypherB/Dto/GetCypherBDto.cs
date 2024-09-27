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
    /// Gets sets of settings
    /// </summary>
    public SettingsDto Settings { get; set; }
}

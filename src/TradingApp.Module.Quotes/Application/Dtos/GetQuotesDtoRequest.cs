using System.ComponentModel;

namespace TradingApp.Module.Quotes.Application.Dtos;

public class GetQuotesDtoRequest
{
    /// <summary>
    ///  Specify technical indicator do include it in combined result
    /// </summary>
    /// <example>Srsi</example>
    public IndicatorsDto[] Indicators { get; set; }

    /// <summary>
    /// Gets or sets the settings for the TimeFrame.
    /// </summary>
    public TimeFrameDto TimeFrame { get; set; }

    /// <summary>
    /// Gets or sets the settings for the Asset.
    /// </summary>
    public AssetDto Asset { get; set; }

    /// <summary>
    /// Gets sets of settings
    /// </summary>
    public SettingsDto Settings { get; set; }
}

public class IndicatorsDto
{
    /// <summary>
    ///  Specify technical indicator do include it in combined result
    /// </summary>
    /// <example>Srsi</example>
    [DefaultValue(nameof(Contract.Constants.TechnicalIndicator.Srsi))]
    public string TechnicalIndicator { get; set; }

    /// <summary>
    ///  Specify technical indicator do include it in combined result
    /// </summary>
    /// <example>Srsi</example>
    [DefaultValue(new string[] { nameof(Contract.Constants.SideIndicator.Ema2x) })]
    public string[] SideIndicators { get; set; }
}


public class SettingsDto
{
    /// <summary>
    /// Gets or sets the settings for the SRSI calculation.
    /// </summary>
    public SrsiSettingsDto SrsiSettings { get; set; }

    /// <summary>
    /// Gets or sets the settings for the RSI calculation.
    /// </summary>
    public RsiSettingsDto RsiSettings { get; set; }

    /// <summary>
    /// Gets or sets the settings for the Mfi calculation.
    /// </summary>
    public MfiSettingsDto MfiSettings { get; set; }

    /// <summary>
    /// Gets or sets the settings for the WaveTrend calculation.
    /// </summary>
    public WaveTrendSettingsDto WaveTrendSettings { get; set; }
}
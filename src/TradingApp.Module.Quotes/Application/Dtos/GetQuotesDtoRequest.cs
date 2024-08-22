using System.ComponentModel;
using TradingApp.Module.Quotes.Contract.Constants;

namespace TradingApp.Module.Quotes.Application.Dtos;

public class GetQuotesDtoRequest
{
    /// <summary>
    ///  Specify technical indicator do include it in combined result
    /// </summary>
    /// <example>Srsi</example>
    [DefaultValue(new string[] { nameof(TechnicalIndicator.Srsi) })]
    public string[] TechnicalIndicators { get; set; }

    /// <summary>
    /// Gets or sets the settings for the TimeFrame.
    /// </summary>
    public TimeFrameDto TimeFrame { get; set; }

    /// <summary>
    /// Gets or sets the settings for the Asset.
    /// </summary>
    public AssetDto Asset { get; set; }

    /// <summary>
    ///  Trading Strategy, chose one appropriate for granularity
    /// </summary>
    /// <example>Scalping</example>
    [DefaultValue(nameof(Features.TradeStrategy.TradingStrategy.Scalping))]
    public string TradingStrategy { get; set; }

    /// <summary>
    /// Gets or sets the settings for the SRSI calculation.
    /// </summary>
    public SrsiSettingsDto SrsiSettings { get; set; }
}

using System.ComponentModel;
using TradingApp.Module.Quotes.Contract.Constants;

namespace TradingApp.Module.Quotes.Application.Dtos;

public class GetQuotesDtoRequest
{
    /// <summary>
    ///  Specify technical indicator do include it in combined result
    /// </summary>
    /// <example>Rsi</example>
    [DefaultValue(new string[] { nameof(TechnicalIndicator.Rsi) })]
    public string[] TechnicalIndicators { get; set; }

    /// <summary>
    /// Gets or sets the settings for the TimeFrame.
    /// </summary>
    public TimeFrameDto TimeFrame { get; set; }

    /// <summary>
    /// Gets or sets the settings for the Asset.
    /// </summary>
    public AssetDto Asset { get; set; }
}

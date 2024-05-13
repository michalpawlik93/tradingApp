using System.ComponentModel;
using TradingApp.Module.Quotes.Contract.Constants;

namespace TradingApp.Module.Quotes.Application.Dtos;

public class GetQuotesDtoRequest : AssetAsParamsDto
{
    /// <summary>
    ///  Specify technical indicator do include it in combined result
    /// </summary>
    /// <example>Rsi</example>
    [DefaultValue(new string[] { nameof(TechnicalIndicator.Rsi) })]
    public string[] TechnicalIndicators { get; set; }

    /// <summary>
    ///  Resample type
    /// </summary>
    /// <example>FiveMins</example>
    [DefaultValue(nameof(Contract.Constants.Granularity.FiveMins))]
    public string Granularity { get; set; }

    /// <summary>
    /// ISO 8601 Required
    /// </summary>
    /// <example>2023-07-09T10:30:00.000Z</example>
    [DefaultValue("2023-07-09T10:30:00.000Z")]
    public string StartDate { get; set; }

    /// <summary>
    /// ISO 8601 Required
    /// </summary>
    /// <example>2023-07-12T10:30:00.000Z</example>
    [DefaultValue("2023-07-12T10:30:00.000Z")]
    public string EndDate { get; set; }
}

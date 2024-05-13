using System.ComponentModel;

namespace TradingApp.Module.Quotes.Application.Dtos;

public class AssetAsParamsDto
{
    /// <summary>
    ///  Asset type
    /// </summary>
    /// <example>Cryptocurrency</example>
    [DefaultValue(nameof(Contract.Constants.AssetType.Cryptocurrency))]
    public string AssetType { get; set; }

    /// <summary>
    ///  Asset name
    /// </summary>
    /// <example>ANC</example>
    [DefaultValue(nameof(Contract.Constants.AssetName.BTCUSD))]
    public string AssetName { get; set; }
}


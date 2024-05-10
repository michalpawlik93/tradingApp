using System.ComponentModel;

namespace TradingApp.Module.Quotes.Application.Dtos;

public class AssetDto
{
    /// <summary>
    ///  Asset name
    /// </summary>
    /// <example>ANC</example>
    [DefaultValue(nameof(Contract.Constants.AssetName.BTCUSD))]
    public string Name { get; set; }
    /// <summary>
    ///  Asset type
    /// </summary>
    /// <example>Cryptocurrency</example>
    [DefaultValue(nameof(Contract.Constants.AssetType.Cryptocurrency))]
    public string Type { get; set; }
}

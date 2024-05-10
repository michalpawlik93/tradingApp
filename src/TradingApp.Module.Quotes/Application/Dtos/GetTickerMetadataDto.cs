using System.ComponentModel;

namespace TradingApp.Module.Quotes.Application.Dtos;

public class GetTickerMetadataDto
{
    /// <summary>
    ///  Ticker type
    /// </summary>
    /// Example value: 3
    [DefaultValue(TingoProvider.Contstants.Ticker.Btcusd)]
    public string Ticker { get; set; }
}

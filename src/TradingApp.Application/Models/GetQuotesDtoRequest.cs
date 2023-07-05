namespace TradingApp.Application.Models;

public class GetQuotesDtoRequest
{
    /// <summary>
    ///  Daily, Hourly, FiveMins
    /// </summary>
    public string Granularity { get; set; }

    /// <summary>
    ///  Cryptocurrency
    /// </summary>
    public string AssetType { get; set; }

    /// <summary>
    ///  ANC
    /// </summary>
    public string AssetName { get; set; }

    /// <summary>
    /// ISO 8601 Required
    /// </summary>
    public string StartDate { get; set; }

    /// <summary>
    /// ISO 8601 Required
    /// </summary>
    public string EndDate { get; set; }
}

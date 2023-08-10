namespace TradingApp.Modules.Quotes.Application.Models;

public class TimeFrameDto
{
    /// <summary>
    /// Gets or sets the time interval for the OHLC data (e.g., 1-minute, 5-minute, etc.).
    /// </summary>
    public string Granularity { get; set; }
    /// <summary>
    /// Gets or sets the start date for the OHLC data.
    /// </summary>
    public string StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date for the OHLC data.
    /// </summary>
    public string EndDate { get; set; }
}

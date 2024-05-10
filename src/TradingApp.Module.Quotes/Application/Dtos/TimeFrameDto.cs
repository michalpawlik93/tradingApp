using System.ComponentModel;

namespace TradingApp.Module.Quotes.Application.Dtos;

public class TimeFrameDto
{
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

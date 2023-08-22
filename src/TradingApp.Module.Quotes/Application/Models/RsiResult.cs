namespace TradingApp.Module.Quotes.Application.Models;

public record RsiResult()
{
    public decimal? Value { get; set; }
    public DateTime Date { get; set; }
}

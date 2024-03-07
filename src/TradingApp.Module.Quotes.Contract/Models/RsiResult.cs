namespace TradingApp.Module.Quotes.Contract.Models;

public record RsiResult()
{
    public decimal? Value { get; set; }
    public DateTime Date { get; set; }
}

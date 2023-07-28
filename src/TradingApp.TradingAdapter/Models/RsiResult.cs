namespace TradingApp.TradingAdapter.Models;

public record RsiResult()
{
    public decimal? Value { get; set; }
    public DateTime Date { get; set; }
}

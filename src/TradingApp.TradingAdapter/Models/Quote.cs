namespace TradingApp.TradingAdapter.Models;

public class Quote
{
    public DateTime Date { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public decimal Volume { get; set; }

    public Quote() { }

    public Quote(DateTime date, decimal open, decimal high, decimal low, decimal close, decimal volume)
    {
        Date = date;
        Open = open;
        High = high;
        Low = low;
        Close = close;
        Volume = volume;
    }
}

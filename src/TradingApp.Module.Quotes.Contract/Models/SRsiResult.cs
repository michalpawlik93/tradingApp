namespace TradingApp.Module.Quotes.Contract.Models;

public record SRsiResult
{
    public DateTime Date { get; set; }
    public decimal? StochK { get; set; }
    public decimal? StochD { get; set; }

    public SRsiResult(DateTime date, decimal? stochK, decimal? stochD)
    {
        StochK = stochK;
        StochD = stochD;
        Date = date;
    }
}

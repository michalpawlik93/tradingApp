﻿namespace TradingApp.TradingAdapter.Models;

internal class StochResult
{
    public StochResult(DateTime date)
    {
        Date = date;
    }
    public decimal? PercentJ { get; set; }
    public decimal? Oscillator { get; set; }
    public decimal? Signal { get; set; }
    public DateTime Date { get; set; }
}

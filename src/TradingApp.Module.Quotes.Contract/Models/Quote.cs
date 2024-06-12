namespace TradingApp.Module.Quotes.Contract.Models;

public readonly record struct Quote(DateTime Date, decimal Open, decimal High, decimal Low, decimal Close, decimal Volume);
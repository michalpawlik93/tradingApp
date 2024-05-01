namespace TradingApp.Module.Quotes.Contract.Models;

public record CryptocurrencyMetadata(
        string Ticker,
        string BaseCurrency,
        string QuoteCurrency,
        string Name,
        string Description);


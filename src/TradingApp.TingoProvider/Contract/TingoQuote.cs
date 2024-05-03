namespace TradingApp.TingoProvider.Contract;

public record TingoQuote(string Ticker,
    string BaseCurrency,
    string QuoteCurrency,
    TingoPriceData[] PriceData);


public record TingoPriceData(decimal Open, decimal High, decimal Low, decimal Close, string Date, float TradesDone, decimal Volume, decimal VolumeNotional);

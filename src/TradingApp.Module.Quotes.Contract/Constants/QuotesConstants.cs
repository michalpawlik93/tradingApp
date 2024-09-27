namespace TradingApp.Module.Quotes.Contract.Constants;

public enum Granularity
{
    Daily, Hourly, FiveMins
}

public enum AssetName
{
    ANC,
    USDPLN,
    BTC,
    CUREBTC,
    BTCUSD,
    EURPLN,
    SPX,
    EURUSD
}

public enum AssetType
{
    Cryptocurrency,
    Currencies,
    Indices
}

public enum MaType
{
    SMA,
}

public enum TechnicalIndicator
{
    Rsi,
    Srsi
}

public enum SideIndicator
{
    SlowFastSrsi,
    SlowSrsi,
    Ema2x
}
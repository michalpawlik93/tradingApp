namespace TradingApp.TingoProvider.Contstants;

public static class Ticker
{
    public const string Curebtc = "curebtc";
    public const string Btcusd = "btcusd";
    public static readonly string[] Tickers = [Curebtc, Btcusd];
}

public static class ResambleFreq
{
    public const string FiveMin = "5min";
    public const string OneHour = "1hour";
    public const string OneDay = "1day";
}

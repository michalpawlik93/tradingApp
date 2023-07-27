using TradingApp.TradingAdapter.Models;

namespace TradingApp.TradingAdapter.Indicators
{
    public static class SRsiIndicator
    {
        public static List<SRsi> Calculate(IEnumerable<Quote> domainQuotes, SRsiSettings settings)
        {
            List<SRsi> srsiList = new List<SRsi>();
            foreach (var domainQuote in domainQuotes)
            {
                var rsi = CalculateRSI(domainQuote.Close, settings.Length);
            }

            return new List<SRsi>();
        }

        private static decimal CalculateRSI(decimal close, int length)
        {
            return 0;
        }
    }
}

//https://pl.tradingview.com/script/T85iFvQj-VuManChu-Cipher-B-Divergences-Strategy/
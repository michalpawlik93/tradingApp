using TradingApp.TradingAdapter.Models;

namespace TradingApp.TradingAdapter.CustomIndexes
{
    public static class SRsiCustom
    {
        public static List<Srsi> Calculate(IEnumerable<DomainQuote> domainQuotes, SRsiSettings settings)
        {
            List<decimal> gains = new List<decimal>();
            List<decimal> losses = new List<decimal>();
            List<decimal> rsiValues = new List<decimal>();

            decimal prevClose = 0;
            int quoteCount = 0;

            foreach (var quote in domainQuotes)
            {
                decimal close = quote.Close;

                if (quoteCount > 0)
                {
                    decimal priceDiff = close - prevClose;
                    if (priceDiff >= 0)
                    {
                        gains.Add(priceDiff);
                        losses.Add(0.0m);
                    }
                    else
                    {
                        gains.Add(0.0m);
                        losses.Add(-priceDiff);
                    }

                    if (quoteCount >= settings.Length)
                    {
                        rsiValues.Add(CalculateSingleRsi(gains, losses));
                    }
                    else
                    {
                        rsiValues.Add(0.0m);
                    }
                }
                else
                {
                    rsiValues.Add(0.0m);
                }

                prevClose = close;
                quoteCount++;
            }

            return new List<Srsi>();
        }

        private static decimal CalculateSingleRsi(List<decimal> gains, List<decimal> losses)
        {
            decimal avgGain = CalculateAverage(gains);
            decimal avgLoss = CalculateAverage(losses);

            if (avgLoss == 0)
            {
                return 100;
            }

            decimal rs = avgGain / avgLoss;
            decimal rsi = 100 - (100 / (1 + rs));
            return rsi;
        }

        private static decimal CalculateAverage(List<decimal> values)
        {
            decimal sum = values.Sum();
            return sum / values.Count;
        }
    }
}

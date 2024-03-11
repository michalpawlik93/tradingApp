using System.Diagnostics.CodeAnalysis;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Evaluator.Indicators;

[ExcludeFromCodeCoverage]
public static class RsiIndicator
{
    public static ICollection<RsiResult> Calculate(List<Quote> tpList, RsiSettings settings)
    {
        ValidateRsi(settings.Length);

        // initialize
        int ohlcLength = tpList.Count;
        decimal avgGain = 0;
        decimal avgLoss = 0;

        List<RsiResult> results = new(ohlcLength);
        decimal[] gain = new decimal[ohlcLength]; // gain
        decimal[] loss = new decimal[ohlcLength]; // loss
        decimal lastValue;

        if (ohlcLength == 0)
        {
            return results;
        }
        else
        {
            lastValue = tpList[0].Close;
        }

        for (int i = 0; i < ohlcLength; i++)
        {
            var r = new RsiResult();
            gain[i] = (tpList[i].Close > lastValue) ? tpList[i].Close - lastValue : 0;
            loss[i] = (tpList[i].Close < lastValue) ? lastValue - tpList[i].Close : 0;
            lastValue = tpList[i].Close;

            // calculate RSI
            if (i > settings.Length)
            {
                avgGain = ((avgGain * (settings.Length - 1)) + gain[i]) / settings.Length;
                avgLoss = ((avgLoss * (settings.Length - 1)) + loss[i]) / settings.Length;

                if (avgLoss > 0)
                {
                    decimal rs = avgGain / avgLoss;
                    r.Value = 100 - (100 / (1 + rs));
                }
                else
                {
                    r.Value = 100;
                }
            }
            // initialize average gain
            else if (i == settings.Length)
            {
                decimal sumGain = 0;
                decimal sumLoss = 0;

                for (int p = 1; p <= settings.Length; p++)
                {
                    sumGain += gain[p];
                    sumLoss += loss[p];
                }

                avgGain = sumGain / settings.Length;
                avgLoss = sumLoss / settings.Length;

                r.Value = (avgLoss > 0) ? 100 - (100 / (1 + (avgGain / avgLoss))) : 100;
            }
            results.Add(r);
        }

        return results;
    }

    private static void ValidateRsi(int length)
    {
        if (length < 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(length),
                length,
                "Length periods must be greater than 0 for RSI."
            );
        }
    }
}
//https://github.com/DaveSkender/Stock.Indicators/blob/main/tests/indicators/m-r/Rsi/Rsi.Tests.cs
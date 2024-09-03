using System.Diagnostics.CodeAnalysis;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Evaluator.Indicators;

[ExcludeFromCodeCoverage]
public static class RsiIndicator
{
    public static IReadOnlyList<RsiResult> Calculate(
        IEnumerable<Quote> tpList,
        RsiSettings settings
    )
    {
        var ohlcLength = tpList.Count();
        decimal avgGain = 0;
        decimal avgLoss = 0;

        List<RsiResult> results = new(ohlcLength);
        var gain = new decimal[ohlcLength]; // gain
        var loss = new decimal[ohlcLength]; // loss
        decimal lastValue;

        if (ohlcLength == 0)
        {
            return results;
        }
        else
        {
            lastValue = tpList.First().Close;
        }

        for (var i = 0; i < ohlcLength; i++)
        {
            var r = new RsiResult();
            var ele = tpList.ElementAt(i);
            gain[i] = (ele.Close > lastValue) ? ele.Close - lastValue : 0;
            loss[i] = (ele.Close < lastValue) ? lastValue - ele.Close : 0;
            lastValue = ele.Close;

            // calculate RSI
            if (i > settings.ChannelLength)
            {
                avgGain =
                    ((avgGain * (settings.ChannelLength - 1)) + gain[i]) / settings.ChannelLength;
                avgLoss =
                    ((avgLoss * (settings.ChannelLength - 1)) + loss[i]) / settings.ChannelLength;

                if (avgLoss > 0)
                {
                    var rs = avgGain / avgLoss;
                    r.Value = 100 - (100 / (1 + rs));
                }
                else
                {
                    r.Value = 100;
                }
            }
            // initialize average gain
            else if (i == settings.ChannelLength)
            {
                decimal sumGain = 0;
                decimal sumLoss = 0;

                for (var p = 1; p <= settings.ChannelLength; p++)
                {
                    sumGain += gain[p];
                    sumLoss += loss[p];
                }

                avgGain = sumGain / settings.ChannelLength;
                avgLoss = sumLoss / settings.ChannelLength;

                r.Value = (avgLoss > 0) ? 100 - (100 / (1 + (avgGain / avgLoss))) : 100;
            }
            results.Add(r);
        }

        return results;
    }
}
//https://github.com/DaveSkender/Stock.Indicators/blob/main/tests/indicators/m-r/Rsi/Rsi.Tests.cs

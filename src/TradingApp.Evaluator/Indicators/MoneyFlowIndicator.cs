using TradingApp.Evaluator.Utils;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Evaluator.Indicators;

/// <summary>
/// Volume is a weight for Vwma
/// </summary>
public static class MoneyFlowIndicator
{
    public static IEnumerable<MfiResult> Calculate(
        IEnumerable<Quote> domainQuotes,
        MfiSettings settings,
        bool scaleResult,
        int resultDecimalPlace
    )
    {
        var vma = CalculateVwma(settings, domainQuotes);

        if (!scaleResult)
            return vma.Select(x => new MfiResult(x)).ToList();

        var scaleFactor = settings.ScaleFactor != 0 ? settings.ScaleFactor : Scale.ByMaxMin(vma);
        return vma.Select(x => new MfiResult(Math.Round(x * scaleFactor, resultDecimalPlace)))
            .ToList();
    }

    /// <summary>
    /// Volume Weighted Moving Average
    /// </summary>
    /// <param name="channelLength"></param>
    /// <param name="domainQuotes"></param>
    /// <returns></returns>
    public static decimal[] CalculateVwma(MfiSettings settings, IEnumerable<Quote> domainQuotes)
    {
        var prices = domainQuotes.Select(q => (q.Close - q.Open) / (q.High - q.Low)).ToArray();
        var smaP = MovingAverage.CalculateSMA(settings.ChannelLength, prices);

        return smaP;
    }

}

using TradingApp.Evaluator.Utils;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Evaluator.Indicators;

/// <summary>
/// Volume is a weight for Vwma
/// </summary>
public static class MoneyFlowIndicator
{
    public static IReadOnlyList<MfiResult> Calculate(
        IReadOnlyList<Quote> domainQuotes,
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
        var prices = domainQuotes
            .Select(q => (q.Close - q.Open) / DivideByNullGuard((q.High - q.Low)))
            .ToArray();
        var smaP = MovingAverage.CalculateSma(settings.ChannelLength, prices);

        return smaP;
    }

    private static decimal DivideByNullGuard(decimal result) => result == 0 ? 1 : result;
}

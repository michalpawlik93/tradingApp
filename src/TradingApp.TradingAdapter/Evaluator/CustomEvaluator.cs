using Skender.Stock.Indicators;
using TradingApp.Common.Utilities;
using TradingApp.TradingAdapter.Constants;
using TradingApp.TradingAdapter.CustomIndexes;
using TradingApp.TradingAdapter.IndexesUtils;
using TradingApp.TradingAdapter.Interfaces;
using TradingApp.TradingAdapter.Mappers;
using TradingApp.TradingAdapter.Models;
using DomainQuote = TradingApp.TradingAdapter.Models.Quote;

namespace TradingApp.TradingAdapter.Evaluator;

public interface ICustomEvaluator : IEvaluator { }

public class CustomEvaluator : ICustomEvaluator
{
    public IEnumerable<decimal?> GetRSI(
        IEnumerable<DomainQuote> domeinQuotes,
        int loockBackPeriod = RsiSettingsConst.DefaultPeriod
    ) =>
        RsiCustom
            .CalculateRsi(domeinQuotes, WaveTrendSettingsConst.AverageLength)
            .Select(r => (decimal?)r);

    public IEnumerable<decimal?> GetMFI(
        IEnumerable<DomainQuote> domeinQuotes,
        int loockBackPeriod = 14
    ) =>
        domeinQuotes
            .MapToSkenderQuotes()
            .GetMfi(loockBackPeriod)
            .Select(r => r.Mfi.ToNullableDecimal());

    public IEnumerable<decimal?> GetVwap(IEnumerable<DomainQuote> domeinQuotes) =>
        VwapCustom.CalculateVwap(domeinQuotes).Select(r => (decimal?)r);

    public IEnumerable<decimal?> GetMomentumWave(
        IEnumerable<DomainQuote> domeinQuotes,
        int loockBackPeriod = RsiSettingsConst.DefaultPeriod
    ) => throw new NotImplementedException();

    public IEnumerable<WaveTrend> GetWaveTrend(IEnumerable<DomainQuote> domainQuotes)
    {
        var ohlcData = domainQuotes.ToList();
        var rsi = RsiCustom.CalculateRsi(ohlcData, WaveTrendSettingsConst.AverageLength);
        var sma = domainQuotes
            .MapToSkenderQuotes()
            .GetSma(WaveTrendSettingsConst.AverageLength)
            .Select(r => r.Sma.TryParse()).ToList();
        var waveTrendValues = WaveTrendCustom.CalculateWaveTrend(
            ohlcData,
            sma,
            WaveTrendSettingsConst.ChannelLength,
            WaveTrendSettingsConst.AverageLength
        );

        return ohlcData.Select((q, index) => CreateWaveTrend(waveTrendValues, rsi, index));
    }

    private WaveTrend CreateWaveTrend(List<decimal> waveTrendValues, List<decimal> rsiValues, int index)
    {
        decimal waveTrend = waveTrendValues[index];
        decimal rsi = rsiValues[index];

        bool isGreenDot = waveTrend > 0;
        bool isRedDot = waveTrend < 0;

        return new WaveTrend(waveTrend, isGreenDot, isRedDot);
    }
}

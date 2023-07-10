using Skender.Stock.Indicators;
using TradingApp.TradingAdapter.Constants;
using TradingApp.TradingAdapter.CustomIndexes;
using TradingApp.TradingAdapter.IndexesUtils;
using TradingApp.TradingAdapter.Interfaces;
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

    public IEnumerable<WaveTrend> GetWaveTrend(IEnumerable<DomainQuote> domeinQuotes)
    {
        var ohlcData = domeinQuotes.ToList();
        var rsi = RsiCustom.CalculateRsi(ohlcData, WaveTrendSettingsConst.AverageLength);
        var sma = SmaCustom.CalculateSma(ohlcData, WaveTrendSettingsConst.AverageLength);
        var waveTrendValues = WaveTrendCustom.CalculateWaveTrend(
            ohlcData,
            rsi,
            WaveTrendSettingsConst.ChannelLength,
            WaveTrendSettingsConst.AverageLength
        );

        return ohlcData.Select((q, index) => CreateWaveTrend(waveTrendValues, sma, index));
    }

    private WaveTrend CreateWaveTrend(
        List<decimal> waveTrendValues,
        List<decimal> rsiValues,
        int index
    )
    {
        decimal waveTrend = waveTrendValues[index];
        decimal rsi = rsiValues[index];
        bool isGreenDot =
            waveTrend > RsiSettingsConst.Overbought && rsi > RsiSettingsConst.Overbought;
        bool isRedDot = waveTrend < RsiSettingsConst.Oversold && rsi < RsiSettingsConst.Oversold;
        return new WaveTrend(waveTrend, isGreenDot, isRedDot);
    }
}

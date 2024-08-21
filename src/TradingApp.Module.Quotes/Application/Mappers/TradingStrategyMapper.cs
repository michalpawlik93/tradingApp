using TradingApp.Module.Quotes.Application.Features.TradeStrategy;

namespace TradingApp.Module.Quotes.Application.Mappers;

public static class TradingStrategyMapper
{
    public static TradingStrategy Map(string tradingStrategy) =>
        Enum.TryParse<TradingStrategy>(tradingStrategy, out var strategy)
            ? strategy
            : TradingStrategy.Scalping;
}

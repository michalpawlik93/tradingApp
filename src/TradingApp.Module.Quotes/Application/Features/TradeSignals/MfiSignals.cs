using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Domain.Enums;

namespace TradingApp.Module.Quotes.Application.Features.TradeSignals;

public static class MfiSignals
{
    // Mfi has different scale, fix it
    public static TradeAction GeMfiTradeAction(MfiResult mfiResult, WaveTrendSettings waveTrendSettings)
    {
        if (mfiResult.Mfi > waveTrendSettings.Overbought)
        {
            return TradeAction.Sell;
        }

        if (mfiResult.Mfi < waveTrendSettings.Oversold)
        {
            return TradeAction.Buy;
        }

        return TradeAction.Hold;
    }
}


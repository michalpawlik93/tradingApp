using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Domain.Enums;

namespace TradingApp.Module.Quotes.Application.Features.TradeSignals;

public static class VwapSignals
{
    public static TradeAction GeVwapTradeAction(WaveTrendSignal waveTrendResult) =>
        waveTrendResult.Vwap > 0 ? TradeAction.Buy : TradeAction.Sell;
}


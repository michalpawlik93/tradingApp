﻿using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.TingoProvider.Contstants;

namespace TradingApp.TingoProvider.Mappers;

public static class TingoDataRequestMapper
{
    public static string Map(this Asset asset) =>
        asset.Name switch
        {
            AssetName.CUREBTC => Ticker.Curebtc,
            AssetName.BTCUSD => Ticker.Btcusd,
            _ => string.Empty
        };

    public static TingoTimeFrame Map(this TimeFrame timeFrame) =>
        new(
            timeFrame.Granularity.Map(),
            timeFrame.StartDate.HasValue ? timeFrame.StartDate.ToString() : null,
            timeFrame.EndDate.HasValue ? timeFrame.EndDate.ToString() : null
        );

    private static string Map(this Granularity granularity) =>
        granularity switch
        {
            Granularity.FiveMins => ResambleFreq.FiveMin,
            Granularity.Hourly => ResambleFreq.OneHour,
            Granularity.Daily => ResambleFreq.OneDay,
            _ => string.Empty
        };
}

public record TingoTimeFrame(string ResampleFreq, string StartDate, string EndDate);

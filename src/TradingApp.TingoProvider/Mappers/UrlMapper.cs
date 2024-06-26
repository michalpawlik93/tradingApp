﻿using System.Text;
using TradingApp.Core.Utilities;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.TingoProvider.Mappers;

public static class UrlMapper
{
    public static string GetCryptoQuotesUri(Asset asset, TimeFrame timeFrame)
    {
        var tingoTimeFrame = timeFrame.Map();
        var sb = new StringBuilder($"tiingo/crypto/prices?tickers={asset.Map()}");
        if (tingoTimeFrame.StartDate != null)
        {
            sb.Append($"&startDate={DateTimeUtils.ConvertDateTimeToIso8601_2String(timeFrame.StartDate.Value)}");
        }
        if (tingoTimeFrame.EndDate != null)
        {
            sb.Append($"&endDate={DateTimeUtils.ConvertDateTimeToIso8601_2String(timeFrame.EndDate.Value)}");
        }
        sb.Append($"&resampleFreq={tingoTimeFrame.ResampleFreq}");
        return sb.ToString();
    }

    public static string GetTickerMetadataUri(Asset asset) =>
        $"tiingo/crypto?tickers={asset.Map()}";
}

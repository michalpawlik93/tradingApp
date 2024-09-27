using FluentResults;
using System.Diagnostics.CodeAnalysis;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.StooqProvider.Constants;

namespace TradingApp.StooqProvider.Utils;

[ExcludeFromCodeCoverage]
public static class FileServiceUtils
{
    public static Result<string> GetZipFilePath(this Granularity granularity) =>
        granularity switch
        {
            Granularity.Daily
                => Path.Combine(
                    StooqFoldersConsts.SubdirectoryPath,
                    Granularity.Daily + StooqFoldersConsts.Extension
                ),
            Granularity.Hourly
                => Path.Combine(
                    StooqFoldersConsts.SubdirectoryPath,
                    Granularity.Hourly + StooqFoldersConsts.Extension
                ),
            Granularity.FiveMins
                => Path.Combine(
                    StooqFoldersConsts.SubdirectoryPath,
                    Granularity.FiveMins + StooqFoldersConsts.Extension
                ),
            _ => Result.Fail(new ValidationError($"Invalid type, {nameof(granularity)}")),
        };

    public static Result<string> GetGranularityPath(this Granularity granularity) =>
        granularity switch
        {
            Granularity.Daily => "daily/",
            Granularity.Hourly => "hourly/",
            Granularity.FiveMins => "5 min/",
            _
                => Result.Fail(
                    new ValidationError($"No existing {nameof(granularity)}: {granularity}")
                ),
        };

    public static Result<string> GetAssetTypePath(this AssetType assetType) =>
        assetType switch
        {
            AssetType.Cryptocurrency => "cryptocurrencies/",
            AssetType.Currencies => "currencies/major/",
            AssetType.Indices => "indices/",
            _ => Result.Fail(new ValidationError($"No existing {nameof(assetType)}: {assetType}")),
        };

    public static Result<string> GetAssetFileName(this AssetName assetName) =>
        assetName switch
        {
            AssetName.ANC => "anc.v.txt",
            AssetName.BTC => "btc.v.txt",
            AssetName.BTCUSD => "btc.v.txt",
            AssetName.USDPLN => "usdpln.txt",
            AssetName.EURPLN => "eurpln.txt",
            AssetName.EURUSD => "eurusd.txt",
            AssetName.SPX => "^spx.txt",
            _ => Result.Fail(new ValidationError($"No existing {nameof(assetName)}: {assetName}")),
        };

    public static Result<string> AncvFilePath(
        Granularity granularity,
        AssetType assetType,
        AssetName assetName
    )
    {
        // Get the path components, ensuring each returns a Result<string>
        var granularityPathResult = granularity.GetGranularityPath();
        if (granularityPathResult.IsFailed)
        {
            return granularityPathResult.ToResult();
        }

        var assetTypePathResult = assetType.GetAssetTypePath();
        if (assetTypePathResult.IsFailed)
        {
            return assetTypePathResult.ToResult();
        }

        var assetFileNameResult = assetName.GetAssetFileName();
        if (assetFileNameResult.IsFailed)
        {
            return assetFileNameResult.ToResult();
        }

        var filePath = Path.Join(
            "data/",
            granularityPathResult.Value,
            "world/",
            assetTypePathResult.Value,
            assetFileNameResult.Value
        );

        return Result.Ok(filePath);
    }

    public static Result<string> FileLocation(Granularity granularity) =>
        granularity switch
        {
            Granularity.Daily => "db/d/?b=d_world_txt",
            Granularity.Hourly => "db/d/?b=h_world_txt",
            _ => Result.Fail(new ValidationError($"Invalid type, nameof(granularity)")),
        };
}

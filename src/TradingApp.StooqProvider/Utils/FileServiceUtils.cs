using System.Diagnostics.CodeAnalysis;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.StooqProvider.Constants;

namespace TradingApp.StooqProvider.Utils;

[ExcludeFromCodeCoverage]
public static class FileServiceUtils
{
    public static string GetZipFilePath(this Granularity granularity) =>
        granularity switch
        {
            Granularity.Daily
                => Path.Combine(
                    StooqFoldersConsts.SubdirectoryPath,
                    Granularity.Daily.ToString() + StooqFoldersConsts.Extension
                ),
            Granularity.Hourly
                => Path.Combine(
                    StooqFoldersConsts.SubdirectoryPath,
                    Granularity.Hourly.ToString() + StooqFoldersConsts.Extension
                ),
            Granularity.FiveMins
                => Path.Combine(
                    StooqFoldersConsts.SubdirectoryPath,
                    Granularity.FiveMins.ToString() + StooqFoldersConsts.Extension
                ),
            _ => throw new ArgumentException("Invalid type", nameof(granularity)),
        };

    public static string GetGranularityPath(this Granularity granularity) =>
        granularity switch
        {
            Granularity.Daily => "daily/",
            Granularity.Hourly => "hourly/",
            Granularity.FiveMins => "5 min/",
            _ => throw new ArgumentException($"No exisiting {nameof(granularity)}: {granularity}"),
        };

    public static string GetAssetTypePath(this AssetType assetType) =>
        assetType switch
        {
            AssetType.Cryptocurrency => "cryptocurrencies/",
            AssetType.Currencies => "currencies/major/",
            _ => throw new ArgumentException($"No exisiting {nameof(assetType)}: {assetType}"),
        };

    public static string GetAssetFileName(this AssetName assetName) =>
        assetName switch
        {
            AssetName.ANC => "anc.v.txt",
            AssetName.BTC => "btc.v.txt",
            AssetName.USDPLN => "usdpln.txt",
            _ => throw new ArgumentException($"No exisiting {nameof(assetName)}: {assetName}"),
        };

    public static string AncvFilePath(
        Granularity granularity,
        AssetType assetType,
        AssetName assetName
    ) =>
        Path.Join(
            "data/",
            granularity.GetGranularityPath(),
            "world/",
            assetType.GetAssetTypePath(),
            assetName.GetAssetFileName()
        );

    public static string FileLocation(Granularity granularity) =>
        granularity switch
        {
            Granularity.Daily => $"db/d/?b=d_world_txt",
            Granularity.Hourly => $"db/d/?b=h_world_txt",
            _ => throw new ArgumentException("Invalid type", nameof(granularity)),
        };
}

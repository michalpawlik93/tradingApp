using System.IO.Compression;
using TradingApp.StooqProvider.Abstraction;
using TradingApp.StooqProvider.Utils;
using TradingApp.TradingAdapter.Enums;

namespace TradingApp.StooqProvider.Services;

internal class ZipArchiveProvider : IZipArchiveProvider
{
    public ZipArchiveEntry? GetEntry(ZipArchive zipArchive, Granularity granularity, AssetType type, AssetName name)
    {
        return zipArchive.GetEntry(FileServiceUtils.AncvFilePath(granularity, type, name));
    }

    public ZipArchive OpenRead(Granularity granularity)
    {
        return ZipFile.OpenRead(granularity.GetZipFilePath());
    }
}

using System.IO.Compression;
using TradingApp.Modules.Domain.Enums;
using TradingApp.StooqProvider.Abstraction;
using TradingApp.StooqProvider.Utils;

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

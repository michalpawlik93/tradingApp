using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.StooqProvider.Abstraction;
using TradingApp.StooqProvider.Utils;

namespace TradingApp.StooqProvider.Services;

[ExcludeFromCodeCoverage]
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

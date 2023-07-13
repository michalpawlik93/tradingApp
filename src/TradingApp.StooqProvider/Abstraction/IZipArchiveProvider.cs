using System.IO.Compression;
using TradingApp.TradingAdapter.Enums;

namespace TradingApp.StooqProvider.Abstraction;

public interface IZipArchiveProvider
{
    ZipArchive OpenRead(Granularity granularity);
    ZipArchiveEntry? GetEntry(ZipArchive zipArchive, Granularity granularity, AssetType type, AssetName name);
}

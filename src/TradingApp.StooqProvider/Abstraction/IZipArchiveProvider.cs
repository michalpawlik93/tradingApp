using System.IO.Compression;
using TradingApp.Module.Quotes.Domain.Enums;

namespace TradingApp.StooqProvider.Abstraction;

public interface IZipArchiveProvider
{
    ZipArchive OpenRead(Granularity granularity);
    ZipArchiveEntry? GetEntry(ZipArchive zipArchive, Granularity granularity, AssetType type, AssetName name);
}

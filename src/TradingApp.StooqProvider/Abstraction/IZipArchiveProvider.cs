using System.IO.Compression;
using TradingApp.Module.Quotes.Contract.Constants;

namespace TradingApp.StooqProvider.Abstraction;

public interface IZipArchiveProvider
{
    ZipArchive OpenRead(Granularity granularity);
    ZipArchiveEntry? GetEntry(ZipArchive zipArchive, Granularity granularity, AssetType type, AssetName name);
}

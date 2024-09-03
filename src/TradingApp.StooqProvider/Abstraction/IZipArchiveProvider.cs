using FluentResults;
using System.IO.Compression;
using TradingApp.Module.Quotes.Contract.Constants;

namespace TradingApp.StooqProvider.Abstraction;

public interface IZipArchiveProvider
{
    Result<ZipArchive> OpenRead(Granularity granularity);
    Result<ZipArchiveEntry> GetEntry(ZipArchive zipArchive, Granularity granularity, AssetType type, AssetName name);
}

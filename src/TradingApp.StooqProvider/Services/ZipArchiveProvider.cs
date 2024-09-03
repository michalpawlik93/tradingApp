using FluentResults;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.StooqProvider.Abstraction;
using TradingApp.StooqProvider.Utils;

namespace TradingApp.StooqProvider.Services;

[ExcludeFromCodeCoverage]
internal class ZipArchiveProvider : IZipArchiveProvider
{
    public Result<ZipArchiveEntry> GetEntry(
        ZipArchive zipArchive,
        Granularity granularity,
        AssetType type,
        AssetName name
    )
    {
        var path = FileServiceUtils.AncvFilePath(granularity, type, name);
        if (path.IsFailed)
        {
            path.ToResult();
        }
        return zipArchive.GetEntry(path.Value);
    }

    public Result<ZipArchive> OpenRead(Granularity granularity)
    {
        var path = granularity.GetZipFilePath();
        if (path.IsFailed)
        {
            path.ToResult();
        }
        return ZipFile.OpenRead(path.Value);
    }
}

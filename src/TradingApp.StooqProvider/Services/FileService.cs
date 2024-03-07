using FluentResults;
using System.Diagnostics.CodeAnalysis;
using TradingApp.Core.Utilities;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.StooqProvider.Abstraction;
using TradingApp.StooqProvider.Constants;
using TradingApp.StooqProvider.Utils;

namespace TradingApp.StooqProvider.Services;

public interface IFileService
{
    Task<Result<ICollection<Quote>>> ReadHistoryQuotaFile(TimeFrame timeFrame, Asset asset);
    Task SaveHistoryQuotaFile(byte[] fileData, TimeFrame timeFrame, Asset asset);
    bool FileExist(TimeFrame timeFrame, Asset asset);
}
public class FileService : IFileService
{
    private readonly IZipArchiveProvider _zipArchiveProvider;
    public FileService(IZipArchiveProvider zipArchiveProvider)
    {
        ArgumentNullException.ThrowIfNull(zipArchiveProvider);
        _zipArchiveProvider = zipArchiveProvider;
    }

    public async Task<Result<ICollection<Quote>>> ReadHistoryQuotaFile(TimeFrame timeFrame, Asset asset)
    {
        using var zipArchive = _zipArchiveProvider.OpenRead(timeFrame.Granularity);
        var zipEntry = _zipArchiveProvider.GetEntry(zipArchive, timeFrame.Granularity, asset.Type, asset.Name);

        if (zipEntry == null)
        {
            return Result.Fail<ICollection<Quote>>($"Can not found file. Path: {FileServiceUtils.AncvFilePath}");
        }
        using var entryStream = zipEntry.Open();
        using var reader = new StreamReader(entryStream);
        string fileContent = await reader.ReadToEndAsync();
        string[] lines = fileContent.Split('\n');

        var quotes = new List<Quote>();
        foreach (var line in lines)
        {
            string[] fields = line.Split(',');
            if (fields.Any() && fields.Length > 8)
            {
                var dateValue = fields[2];
                var timeValue = fields[3];
                decimal.TryParse(fields[4], out var openValue);
                decimal.TryParse(fields[5], out var highValue);
                decimal.TryParse(fields[6], out var lowValue);
                decimal.TryParse(fields[7], out var closeValue);
                decimal.TryParse(fields[8], out var volumeValue);
                var dateTimeValue = DateTimeUtils.ParseDateTime(dateValue, timeValue);
                if (dateTimeValue != DateTime.MinValue)
                {
                    quotes.Add(new Quote(dateTimeValue, openValue, highValue, lowValue, closeValue, volumeValue));
                }
            }
        }
        return Result.Ok<ICollection<Quote>>(quotes);
    }

    [ExcludeFromCodeCoverage]
    public async Task SaveHistoryQuotaFile(byte[] fileData, TimeFrame timeFrame, Asset asset)
    {
        if (!Directory.Exists(StooqFoldersConsts.SubdirectoryPath))
        {
            Directory.CreateDirectory(StooqFoldersConsts.SubdirectoryPath);
        }
        await File.WriteAllBytesAsync(timeFrame.Granularity.GetZipFilePath(), fileData);
    }

    [ExcludeFromCodeCoverage]
    public bool FileExist(TimeFrame timeFrame, Asset asset) => File.Exists(timeFrame.Granularity.GetZipFilePath());
}

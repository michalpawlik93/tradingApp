using FluentResults;
using System.Globalization;
using System.IO.Compression;
using TradingApp.TradingAdapter.Enums;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.StooqProvider.Services;

public interface IFileService
{
    Task<Result<ICollection<Quote>>> ReadHistoryQuotaFile(TimeFrame timeFrame, Asset asset);
    Task SaveHistoryQuotaFile(byte[] fileData, TimeFrame timeFrame, Asset asset);
    bool FileExist(TimeFrame timeFrame, Asset asset);
}
public class FileService : IFileService
{

    private const string SubdirectoryPath = "history";
    private const string Extension = "stooq.zip";

    public async Task<Result<ICollection<Quote>>> ReadHistoryQuotaFile(TimeFrame timeFrame, Asset asset)
    {
        using (var zipArchive = ZipFile.OpenRead(ZipFilePath(timeFrame.Granularity)))
        {
            var zipEntry = zipArchive.GetEntry(AncvFilePath(timeFrame.Granularity, asset.Type, asset.Name));

            if (zipEntry == null)
            {
                return Result.Fail<ICollection<Quote>>($"Can not found file. Path: {AncvFilePath}");
            }
            using (Stream entryStream = zipEntry.Open())
            using (StreamReader reader = new StreamReader(entryStream))
            {
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
                        var dateTimeValue = ParseDateTime(dateValue, timeValue);
                        if (dateTimeValue != DateTime.MinValue)
                        {
                            quotes.Add(new Quote(dateTimeValue, openValue, highValue, lowValue, closeValue, volumeValue));
                        }
                    }
                }
                return Result.Ok<ICollection<Quote>>(quotes);
            }
        }
    }

    public async Task SaveHistoryQuotaFile(byte[] fileData, TimeFrame timeFrame, Asset asset)
    {
        if (!Directory.Exists(SubdirectoryPath))
        {
            Directory.CreateDirectory(SubdirectoryPath);
        }
        await File.WriteAllBytesAsync(ZipFilePath(timeFrame.Granularity), fileData);
    }

    public bool FileExist(TimeFrame timeFrame, Asset asset) => File.Exists(ZipFilePath(timeFrame.Granularity));

    private string ZipFilePath(Granularity granularity) =>
       granularity switch
       {
           Granularity.Daily => Path.Combine(SubdirectoryPath, Granularity.Daily.ToString() + Extension),
           Granularity.Hourly => Path.Combine(SubdirectoryPath, Granularity.Hourly.ToString() + Extension),
           Granularity.FiveMins => Path.Combine(SubdirectoryPath, Granularity.FiveMins.ToString() + Extension),
           _ => throw new ArgumentException("Invalid type", nameof(granularity)),
       };

    private static string GranularityPath(Granularity granularity) =>
       granularity switch
       {
           Granularity.Daily => "daily/",
           Granularity.Hourly => "hourly/",
           Granularity.FiveMins => "5 min/",
           _ => throw new ArgumentException($"No exisiting {nameof(granularity)}: {granularity}"),
       };

    private static string AssetTypePath(AssetType assetType) =>
       assetType switch
       {
           AssetType.Cryptocurrency => "cryptocurrencies/",
           _ => throw new ArgumentException($"No exisiting {nameof(assetType)}: {assetType}"),
       };
    private static string AssetFileName(AssetName assetName) =>
       assetName switch
       {
           AssetName.ANC => "anc.v.txt",
           _ => throw new ArgumentException($"No exisiting {nameof(assetName)}: {assetName}"),
       };

    private static string AncvFilePath(Granularity granularity, AssetType assetType, AssetName assetName) => Path.Join("data/", GranularityPath(granularity), "world/", AssetTypePath(assetType), AssetFileName(assetName));

    private static DateTime ParseDateTime(string dateInput, string timeInput)
    {
        var dateParsed = DateTime.TryParseExact(dateInput, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate);
        var timeParsed = DateTime.TryParseExact(timeInput, "HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTime);
        if (dateParsed && timeParsed)
        {
            return parsedDate.Date + parsedTime.TimeOfDay;
        }

        if (dateParsed)
        {
            return parsedDate.Date;
        }

        if (timeParsed)
        {
            return parsedTime;
        }

        return DateTime.MinValue;
    }
}

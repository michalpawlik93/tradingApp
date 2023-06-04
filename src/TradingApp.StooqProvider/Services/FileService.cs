using FluentResults;
using System.Globalization;
using System.IO.Compression;
using TradingApp.TradingAdapter.Enums;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.StooqProvider.Services;

public interface IFileService
{
    Task<Result<IEnumerable<Quote>>> ReadHistoryQuotaFile(HistoryType type);
    Task SaveHistoryQuotaFile(byte[] fileData, HistoryType type);
    bool FileExist(HistoryType type);
}
public class FileService : IFileService
{

    private const string SubdirectoryPath = "history";
    private const string Extension = "stooq.zip";

    public async Task<Result<IEnumerable<Quote>>> ReadHistoryQuotaFile(HistoryType type)
    {
        using (var zipArchive = ZipFile.OpenRead(ZipFilePath(type)))
        {
            var zipEntry = zipArchive.GetEntry(AncvFilePath);

            if (zipEntry == null)
            {
                return Result.Fail<IEnumerable<Quote>>($"Can not found file. Path: {AncvFilePath}");
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
                            quotes.Add(new Quote(ParseDateTime(dateValue, timeValue), openValue, highValue, lowValue, closeValue, volumeValue));
                        }
                    }
                }
                return Result.Ok(quotes.AsEnumerable());
            }
        }
    }

    public async Task SaveHistoryQuotaFile(byte[] fileData, HistoryType type)
    {
        if (!Directory.Exists(SubdirectoryPath))
        {
            Directory.CreateDirectory(SubdirectoryPath);
        }
        await File.WriteAllBytesAsync(ZipFilePath(type), fileData);
    }

    public bool FileExist(HistoryType type) => File.Exists(ZipFilePath(type));

    private string ZipFilePath(HistoryType type) =>
       type switch
       {
           HistoryType.Daily => Path.Combine(SubdirectoryPath, HistoryType.Daily.ToString() + Extension),
           HistoryType.Hourly => Path.Combine(SubdirectoryPath, HistoryType.Hourly.ToString() + Extension),
           _ => throw new ArgumentException("Invalid type", nameof(type)),
       };


    private static string AncvFilePath => Path.Join("data/", "daily/", "world/", "cryptocurrencies/", "anc.v.txt");

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

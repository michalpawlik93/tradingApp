using FluentResults;
using System.IO.Compression;
using TradingApp.TradingAdapter.Enums;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.StooqProvider.Services;

public interface IFileService
{
    Task<Result<IEnumerable<Quote>>> ReadHistoryQuotaFile(HistoryType type);
    Task SaveHistoryQuotaFile(byte[] fileData, HistoryType type);
}
public class FileService : IFileService
{
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
                var result = lines.Select((l) =>
                {
                    string[] fields = l.Split(',');
                    //string ticker = fields[0];
                    //string per = fields[1];
                    string date = fields[2];
                    //string time = fields[3];
                    decimal open = decimal.Parse(fields[4]);
                    decimal high = decimal.Parse(fields[5]);
                    decimal low = decimal.Parse(fields[6]);
                    decimal close = decimal.Parse(fields[7]);
                    decimal volume = decimal.Parse(fields[8]);
                    return new Quote(DateTime.Parse(date), open, high, low, close, volume);
                });
                return Result.Ok(result);
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

    private const string SubdirectoryPath = "history";
    private const string Extension = "stooq.zip";
    private string ZipFilePath(HistoryType type) =>
       type switch
       {
           HistoryType.Daily => Path.Combine(SubdirectoryPath, HistoryType.Daily.ToString() + Extension),
           HistoryType.Hourly => Path.Combine(SubdirectoryPath, HistoryType.Hourly.ToString() + Extension),
           _ => throw new ArgumentException("Invalid type", nameof(type)),
       };
    private static string AncvFilePath => Path.Combine("data", "daily", "world", "cryptocurrencies", "anc.v.txt");
}

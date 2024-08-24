using FluentResults;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using TradingApp.Core.Models;
using TradingApp.Core.Utilities;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.StooqProvider.Abstraction;
using TradingApp.StooqProvider.Utils;

namespace TradingApp.StooqProvider.Services;

public interface IFileService
{
    Task<Result<IReadOnlyList<Quote>>> ReadHistoryQuotaFile(TimeFrame timeFrame, Asset asset);
}

[ExcludeFromCodeCoverage]
public class FileService : IFileService
{
    private readonly IZipArchiveProvider _zipArchiveProvider;

    public FileService(IZipArchiveProvider zipArchiveProvider)
    {
        ArgumentNullException.ThrowIfNull(zipArchiveProvider);
        _zipArchiveProvider = zipArchiveProvider;
    }

    public async Task<Result<IReadOnlyList<Quote>>> ReadHistoryQuotaFile(
        TimeFrame timeFrame,
        Asset asset
    )
    {
        using var zipArchive = _zipArchiveProvider.OpenRead(timeFrame.Granularity);
        var zipEntry = _zipArchiveProvider.GetEntry(
            zipArchive,
            timeFrame.Granularity,
            asset.Type,
            asset.Name
        );

        if (zipEntry == null)
        {
            return Result.Fail<IReadOnlyList<Quote>>(
                new ValidationError($"Can not find file. Path: {FileServiceUtils.AncvFilePath}")
            );
        }

        await using var entryStream = zipEntry.Open();
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
                decimal.TryParse(fields[4], CultureInfo.InvariantCulture, out var openValue);
                decimal.TryParse(fields[5], CultureInfo.InvariantCulture, out var highValue);
                decimal.TryParse(fields[6], CultureInfo.InvariantCulture, out var lowValue);
                decimal.TryParse(fields[7], CultureInfo.InvariantCulture, out var closeValue);
                decimal.TryParse(fields[8], CultureInfo.InvariantCulture, out var volumeValue);
                var dateTimeValue = DateTimeUtils.ParseDateTime(dateValue, timeValue);
                if (dateTimeValue != DateTime.MinValue)
                {
                    quotes.Add(
                        new Quote(
                            dateTimeValue,
                            openValue,
                            highValue,
                            lowValue,
                            closeValue,
                            volumeValue
                        )
                    );
                }
            }
        }
        return Result.Ok<IReadOnlyList<Quote>>(quotes);
    }
}

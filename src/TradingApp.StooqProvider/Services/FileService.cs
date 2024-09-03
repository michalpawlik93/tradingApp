using FluentResults;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using TradingApp.Core.Utilities;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.StooqProvider.Abstraction;

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
        var zipArchiveResult = _zipArchiveProvider.OpenRead(timeFrame.Granularity);
        if (zipArchiveResult.IsFailed)
        {
            return zipArchiveResult.ToResult();
        }

        using var zipArchive = zipArchiveResult.Value;

        var zipEntryResult = _zipArchiveProvider.GetEntry(
            zipArchive,
            timeFrame.Granularity,
            asset.Type,
            asset.Name
        );

        if (zipEntryResult.IsFailed)
        {
            return zipEntryResult.ToResult();
        }

        var zipEntry = zipEntryResult.Value;

        await using var entryStream = zipEntry.Open();
        using var reader = new StreamReader(entryStream);
        var fileContent = await reader.ReadToEndAsync();
        var lines = fileContent.Split('\n');

        var quotes = new List<Quote>();
        foreach (var line in lines)
        {
            var fields = line.Split(',');
            if (fields.Length <= 8)
                continue;
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

        return Result.Ok<IReadOnlyList<Quote>>(quotes);
    }
}

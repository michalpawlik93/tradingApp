using FluentResults;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Ports;
using TradingApp.StooqProvider.Services;
using TradingApp.StooqProvider.Setup;
using TradingApp.StooqProvider.Utils;

namespace TradingApp.StooqProvider;

public interface IStooqProvider : ITradingAdapter { };

[ExcludeFromCodeCoverage]
public sealed class StooqProvider : TradingAdapterAbstract, IStooqProvider
{
    private const string LocationHeaderKey = "Location";
    private readonly IFileService _fileService;
    private StooqClient _stooqClient { get; set; }

    public StooqProvider(StooqClient stooqClient, IFileService fileService)
    {
        ArgumentNullException.ThrowIfNull(stooqClient);
        _stooqClient = stooqClient;
        ArgumentNullException.ThrowIfNull(fileService);
        _fileService = fileService;
    }

    protected override Task<Result<AuthorizeResponse>> AuthorizeAsync(AuthorizeRequest request) => throw new NotImplementedException();

    protected override Task<Result> LogoutAsync() => throw new NotImplementedException();

    protected override async Task<Result<ICollection<Quote>>> GetQuotesAsync(TimeFrame timeFrame, Asset asset) =>
        await _fileService.ReadHistoryQuotaFile(timeFrame, asset);


    //Cant save files due to security problems
    protected override async Task<Result> SaveQuotesAsync(TimeFrame timeFrame, Asset asset, bool overrideFile)
    {
        if (!overrideFile && _fileService.FileExist(timeFrame, asset))
        {
            return Result.Ok().WithSuccess($"File exists. OverrideFileFlag: {overrideFile}");
        }
        var getLocationResponse = await _stooqClient.Client.GetAsync(FileServiceUtils.FileLocation(timeFrame.Granularity));
        getLocationResponse.EnsureSuccessStatusCode();
        if (getLocationResponse.StatusCode != HttpStatusCode.Found)
        {
            return Result.Fail($"{getLocationResponse.StatusCode} status is not equal {HttpStatusCode.Found}");
        }

        if (!getLocationResponse.Content.Headers.TryGetValues(LocationHeaderKey, out var values) || values == null)
        {

            return Result.Fail($"{LocationHeaderKey} can not be found in response header.");
        }
        var locationValue = values.FirstOrDefault();
        if (string.IsNullOrEmpty(locationValue))
        {
            return Result.Fail($"{LocationHeaderKey} value is null or empty.");
        }
        byte[] fileData = await _stooqClient.Client.GetByteArrayAsync($"https:{locationValue}");
        await _fileService.SaveHistoryQuotaFile(fileData, timeFrame, asset);
        return Result.Ok();
    }
}

using FluentResults;
using Microsoft.Extensions.Logging;
using System.Net;
using TradingApp.StooqProvider.Services;
using TradingApp.StooqProvider.Setup;
using TradingApp.TradingAdapter.Enums;
using TradingApp.TradingAdapter.Interfaces;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.StooqProvider;


public interface IStooqProvider : ITradingAdapter { };
public sealed class StooqProvider : TradingAdapterAbstract, IStooqProvider
{
    private readonly ILogger<StooqProvider> _logger;
    private readonly IFileService _fileService;
    private StooqClient _stooqClient { get; set; }

    public StooqProvider(ILogger<StooqProvider> logger, StooqClient stooqClient, IFileService fileService) : base(logger)
    {
        ArgumentNullException.ThrowIfNull(logger);
        _logger = logger;
        ArgumentNullException.ThrowIfNull(stooqClient);
        _stooqClient = stooqClient;
        ArgumentNullException.ThrowIfNull(fileService);
        _fileService = fileService;
    }

    protected override Task<Result<AuthorizeResponse>> AuthorizeAsync(AuthorizeRequest request) => throw new NotImplementedException();

    protected override Task<Result> LogoutAsync() => throw new NotImplementedException();

    protected override async Task<Result<IEnumerable<Quote>>> GetQuotesAsync(HistoryType type) =>
        await _fileService.ReadHistoryQuotaFile(type);

    protected override async Task<Result> SaveQuotesAsync(HistoryType type, bool overrideFile)
    {
        if (!overrideFile && _fileService.FileExist(type))
        {
            return Result.Ok().WithSuccess($"File exists. OverrideFileFlag: {overrideFile}");
        }
        var getLocationResponse = await _stooqClient.Client.GetAsync(FileLocation(type));
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
        await _fileService.SaveHistoryQuotaFile(fileData, type);
        return Result.Ok();
    }



    private const string LocationHeaderKey = "Location";
    private string FileLocation(HistoryType type) =>
       type switch
       {
           HistoryType.Daily => $"db/d/?b=d_world_txt",
           HistoryType.Hourly => $"db/d/?b=h_world_txt",
           _ => throw new ArgumentException("Invalid type", nameof(type)),
       };
}

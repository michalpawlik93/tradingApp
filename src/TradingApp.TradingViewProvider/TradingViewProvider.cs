using FluentResults;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using TradingApp.Common.Utilities;
using TradingApp.TradingAdapter.Enums;
using TradingApp.TradingAdapter.Interfaces;
using TradingApp.TradingAdapter.Models;
using TradingApp.TradingViewProvider.Constants;
using TradingApp.TradingViewProvider.Contract;
using TradingApp.TradingViewProvider.Mappers;
using TradingApp.TradingViewProvider.Setup;
using TradingApp.TradingViewProvider.Utils;

namespace TradingApp.TradingViewProvider;

public interface ITradingViewProvider : ITradingAdapter { };
public sealed class TradingViewProvider : TradingAdapterAbstract, ITradingViewProvider
{
    private readonly ILogger<TradingViewProvider> _logger;
    private TradingViewClient _tradingViewClient { get; set; }

    public TradingViewProvider(ILogger<TradingViewProvider> logger, TradingViewClient tradingViewClient) : base(logger)
    {
        ArgumentNullException.ThrowIfNull(logger);
        _logger = logger;
        ArgumentNullException.ThrowIfNull(tradingViewClient);
        _tradingViewClient = tradingViewClient;
    }

    public string ProviderName => "TradingView";

    protected override async Task<Result<AuthorizeResponse>> AuthorizeAsync(AuthorizeRequest request)
    {
        try
        {
            var content = HttpUtilities.ConvertToUrlEncoded(TvAuthorizeMapper.Map(request));
            var httpResponse = await _tradingViewClient.Client.PostAsync(TvUri.Authorize, content);
            var response = await DeserializeHttpResponse<ServiceResponse<TvAuthorizeResponse>>(httpResponse);
            return response.GetResult<TvAuthorizeResponse, AuthorizeResponse>(TvAuthorizeMapper.Map);
        }
        catch (Exception)
        {
            _logger.LogError(TradingViewProviderErrorMessages.ExceptionErrorMessage);
            throw;
        }
    }

    protected override async Task<Result> LogoutAsync()
    {
        try
        {
            var httpResponse = await _tradingViewClient.Client.PostAsync(TvUri.Authorize, null);
            var response = await DeserializeHttpResponse<ServiceResponseBase>(httpResponse);
            return response.GetResult();
        }
        catch (Exception)
        {
            _logger.LogError(TradingViewProviderErrorMessages.ExceptionErrorMessage);
            throw;
        }
    }

    public static class TradingViewProviderErrorMessages
    {
        public const string ExceptionErrorMessage = "TradingViewProvider exception.";
    }

    public async Task<T> DeserializeHttpResponse<T>(HttpResponseMessage response)
    {
        var responseData = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseData);
    }

    protected override Task<Result<IEnumerable<Quote>>> GetQuotesAsync(HistoryType type)
    {
        throw new NotImplementedException();
    }

    protected override Task<Result> SaveQuotesAsync(HistoryType type)
    {
        throw new NotImplementedException();
    }
}

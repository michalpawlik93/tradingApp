using FluentResults;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using TradingApp.Core.Utilities;
using TradingApp.TradingViewProvider.Abstraction;
using TradingApp.TradingViewProvider.Constants;
using TradingApp.TradingViewProvider.Contract;
using TradingApp.TradingViewProvider.Setup;
using TradingApp.TradingViewProvider.Utils;

namespace TradingApp.TradingViewProvider;

public sealed class TradingViewProvider : ITradingViewProvider
{
    private readonly ILogger<TradingViewProvider> _logger;
    private TradingViewClient _tradingViewClient { get; set; }

    public TradingViewProvider(
        ILogger<TradingViewProvider> logger,
        TradingViewClient tradingViewClient
    )
    {
        ArgumentNullException.ThrowIfNull(logger);
        _logger = logger;
        ArgumentNullException.ThrowIfNull(tradingViewClient);
        _tradingViewClient = tradingViewClient;
    }

    public async Task<Result<TvAuthorizeResponse>> Authorize(TvAuthorizeRequest request)
    {
        try
        {
            var content = HttpUtilities.ConvertToUrlEncoded(request);
            var httpResponse = await _tradingViewClient.Client.PostAsync(TvUri.Authorize, content);
            var response = await DeserializeHttpResponse<ServiceResponse<TvAuthorizeResponse>>(
                httpResponse
            );
            ArgumentNullException.ThrowIfNull(response);
            return response.GetResult();
        }
        catch (Exception)
        {
            _logger.LogError(TradingViewProviderErrorMessages.ExceptionErrorMessage);
            throw;
        }
    }

    public async Task<Result> Logout()
    {
        try
        {
            var httpResponse = await _tradingViewClient.Client.PostAsync(TvUri.Authorize, null);
            var response = await DeserializeHttpResponse<ServiceResponseBase>(httpResponse);
            ArgumentNullException.ThrowIfNull(response);
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

    public async Task<T?> DeserializeHttpResponse<T>(HttpResponseMessage response)
    {
        var responseData = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseData);
    }
}

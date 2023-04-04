using FluentResults;
using Microsoft.Extensions.Logging;
using TradingApp.Application.Utilities;
using TradingApp.TradingAdapter.Interfaces;
using TradingApp.TradingAdapter.Models;
using TradingApp.TradingViewProvider.Constants;
using TradingApp.TradingViewProvider.Contract;
using TradingApp.TradingViewProvider.Mappers;
using TradingApp.TradingViewProvider.Utils;

namespace TradingApp.TradingViewProvider;

internal class TradingViewProvider : IProvider
{
    private readonly ILogger<TradingViewProvider> _logger;
    private HttpClient Client { get; set; }

    public TradingViewProvider(ILogger<TradingViewProvider> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public string ProviderName => "TradingView";
    public void CreateClient(HttpClient client)
    {
        Client = client;
    }

    public async Task<Result<AuthorizeResponse>> Authorize(AuthorizeRequest request)
    {
        try
        {
            var content = HttpUtilities.ConvertToUrlEncoded(TvAuthorizeMapper.Map(request));
            var httpResponse = await Client.PostAsync(TvUri.Authorize, content);
            var response = HttpUtilities.DeserializeHttpResponse<ServiceResponse<TvAuthorizeResponse>>(httpResponse);
            return response.GetResult<TvAuthorizeResponse, AuthorizeResponse>(TvAuthorizeMapper.Map);
        }
        catch(Exception) 
        {
            _logger.LogError(TradingViewProviderErrorMessages.ExceptionErrorMessage);
            throw;
        }
    }

    public async Task<Result> Logout()
    {
        try
        {
            var httpResponse = await Client.PostAsync(TvUri.Authorize, null);
            var response = HttpUtilities.DeserializeHttpResponse<ServiceResponseBase>(httpResponse);
            return response.GetResult();
        }
        catch (Exception)
        {
            _logger.LogError(TradingViewProviderErrorMessages.ExceptionErrorMessage);
            throw;
        }
    }
}

public static class TradingViewProviderErrorMessages
{
    public const string ExceptionErrorMessage = "TradingViewProvider exception.";
}

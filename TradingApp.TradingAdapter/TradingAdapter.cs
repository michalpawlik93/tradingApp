using FluentResults;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TradingApp.TradingAdapter.Interfaces;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.TradingAdapter;

public class TradingAdapter : ITradingAdapter
{
    private readonly IProvider _provider;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<ProviderConfiguration> _providerOptions;
    private readonly ILogger<TradingAdapter> _logger;

    public TradingAdapter(IProvider provider, IHttpClientFactory httpClientFactory, IOptions<ProviderConfiguration> providerOptions,
        ILogger<TradingAdapter> logger)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _providerOptions = providerOptions ?? throw new ArgumentNullException(nameof(providerOptions));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task CreateClient(string providerName)
    {
        _logger.LogInformation("Create client for {providerName} started.", providerName);
        var client = _httpClientFactory.CreateClient(providerName);
         _provider.CreateClient(client);
        _logger.LogInformation("Client for {providerName} created.", providerName);
    }

    public async Task<Result<AuthorizeResponse>> Authorize(AuthorizeRequest request)
    {
        return await _provider.Authorize(request);
    }

    public async Task<Result> Logout()
    {
        return await _provider.Logout();
    }
}

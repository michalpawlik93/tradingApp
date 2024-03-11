using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace TradingApp.TradingViewProvider.Setup;

[ExcludeFromCodeCoverage]
public class TradingViewClient
{
    public HttpClient Client { get; }
    public TradingViewClient(HttpClient client, IOptions<TradingViewClientConfig> options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(options.Value.BaseUrl);
        Client = client;
        Client.BaseAddress = new Uri(options.Value.BaseUrl);
        Client.Timeout = new TimeSpan(0, 0, 30);
        Client.DefaultRequestHeaders.Clear();
    }
}

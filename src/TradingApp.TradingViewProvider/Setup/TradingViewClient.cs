using Microsoft.Extensions.Options;

namespace TradingApp.TradingViewProvider.Setup;

public class TradingViewClient
{
    public HttpClient Client { get; }
    public TradingViewClient(HttpClient client, IOptions<TradingViewClientConfig> options)
    {
        ArgumentNullException.ThrowIfNull(options);
        Client = client;
        Client.BaseAddress = new Uri(options.Value.BaseUrl);
        Client.Timeout = new TimeSpan(0, 0, 30);
        Client.DefaultRequestHeaders.Clear();
    }
}

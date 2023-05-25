using Microsoft.Extensions.Options;

namespace TradingApp.StooqProvider.Setup;

public class StooqClient
{
    public HttpClient Client { get; }
    public StooqClient(HttpClient client, IOptions<StooqClientConfig> options)
    {
        ArgumentNullException.ThrowIfNull(options);
        Client = client;
        Client.BaseAddress = new Uri(options.Value.BaseUrl);
        Client.Timeout = new TimeSpan(0, 0, 30);
        Client.DefaultRequestHeaders.Clear();
    }
}

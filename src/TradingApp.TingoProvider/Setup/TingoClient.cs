using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace TradingApp.TingoProvider.Setup;

[ExcludeFromCodeCoverage]
public class TingoClient
{
    public HttpClient Client { get; }
    public TingoClient(HttpClient client, IOptions<TingoClientConfig> options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(options.Value.BaseUrl);
        Client = client;
        Client.BaseAddress = new Uri(options.Value.BaseUrl);
        Client.Timeout = new TimeSpan(0, 0, 30);
        Client.DefaultRequestHeaders.Clear();
        Client.DefaultRequestHeaders.Add("Authorization", $"Token {options.Value.Token}");
        Client.DefaultRequestHeaders.Add("User-Agent", "agent");
    }
}


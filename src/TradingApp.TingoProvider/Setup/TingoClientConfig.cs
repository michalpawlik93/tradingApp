using System.Diagnostics.CodeAnalysis;

namespace TradingApp.TingoProvider.Setup;

[ExcludeFromCodeCoverage]
public class TingoClientConfig
{
    public const string ConfigSectionName = "TingoClient";
    public string? BaseUrl { get; set; }
    public string? Token { get; set; }
}

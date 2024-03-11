using System.Diagnostics.CodeAnalysis;

namespace TradingApp.TradingViewProvider.Constants;

[ExcludeFromCodeCoverage]
public static class TvUri
{
    public const string Base = "https://www.tradingview.com/api/";
    public const string Authorize = $"{Base}authorize";
    public const string Logout = $"{Base}logout";
}

using System.Diagnostics.CodeAnalysis;

namespace TradingApp.TradingViewProvider.Contract;

[ExcludeFromCodeCoverage]
public record TvAuthorizeResponse(string access_token, string expiration);

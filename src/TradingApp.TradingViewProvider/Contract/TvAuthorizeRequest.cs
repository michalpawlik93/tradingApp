using System.Diagnostics.CodeAnalysis;

namespace TradingApp.TradingViewProvider.Contract;

[ExcludeFromCodeCoverage]
public record TvAuthorizeRequest(string login, string password, string locale);

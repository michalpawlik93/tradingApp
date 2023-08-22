using System.Diagnostics.CodeAnalysis;
using TradingApp.Modules.Application.Models;
using TradingApp.TradingViewProvider.Constants;
using TradingApp.TradingViewProvider.Contract;

namespace TradingApp.TradingViewProvider.Mappers;

[ExcludeFromCodeCoverage]
public static class TvAuthorizeMapper
{
    public static TvAuthorizeRequest Map(AuthorizeRequest model) =>
        new(model.Login, model.Password, Locale.PL);

    public static AuthorizeResponse Map(TvAuthorizeResponse model) =>
        new(model.access_token, model.expiration);
}

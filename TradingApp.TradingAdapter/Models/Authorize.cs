namespace TradingApp.TradingAdapter.Models;

public record AuthorizeRequest(string Login, string Password);

public record AuthorizeResponse(string AccessToken, string Expiration);
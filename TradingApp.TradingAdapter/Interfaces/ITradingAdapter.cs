using FluentResults;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.TradingAdapter.Interfaces;

public interface ITradingAdapter
{
    Task<Result<AuthorizeResponse>> Authorize(AuthorizeRequest request);
    Task<Result> Logout();
}

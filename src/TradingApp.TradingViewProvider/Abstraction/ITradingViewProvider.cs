using FluentResults;
using TradingApp.TradingViewProvider.Contract;

namespace TradingApp.TradingViewProvider.Abstraction;

public interface ITradingViewProvider
{
    Task<Result<TvAuthorizeResponse>> Authorize(TvAuthorizeRequest request);
    Task<Result> Logout();
}

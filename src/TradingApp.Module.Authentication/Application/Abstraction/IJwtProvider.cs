using FluentResults;
using TradingApp.Module.Quotes.Authentication.Models;

namespace TradingApp.Module.Quotes.Authentication.Abstraction;

public interface IJwtProvider
{
    Result<string> Generate(User user);
}

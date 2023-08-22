using FluentResults;
using TradingApp.Modules.Authentication.Models;

namespace TradingApp.Modules.Authentication.Abstraction;

public interface IJwtProvider
{
    Result<string> Generate(User user);
}

using FluentResults;
using TradingApp.Modules.Models;

namespace TradingApp.Modules.Abstraction;

public interface IJwtProvider
{
    Result<string> Generate(User user);
}

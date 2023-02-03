using FluentResults;
using TradingApp.Application.Models;

namespace TradingApp.Application.Abstraction;

public interface IJwtProvider
{
    Result<string> Generate(User user);
}

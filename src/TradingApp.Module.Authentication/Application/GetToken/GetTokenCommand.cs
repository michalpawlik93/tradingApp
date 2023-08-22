using MediatR;
using TradingApp.Core.Models;
using TradingApp.Modules.Authentication.Models;

namespace TradingApp.Modules.Authentication.GetToken;

public record GetTokenCommand(User user) : IRequest<ServiceResponse<string>>;

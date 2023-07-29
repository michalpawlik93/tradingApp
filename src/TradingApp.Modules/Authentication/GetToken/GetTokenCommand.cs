using MediatR;
using TradingApp.Modules.Models;

namespace TradingApp.Modules.Authentication.GetToken;

public record GetTokenCommand(User user) : IRequest<ServiceResponse<string>>;

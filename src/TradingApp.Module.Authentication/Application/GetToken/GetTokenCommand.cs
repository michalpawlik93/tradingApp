using MediatR;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Authentication.Models;

namespace TradingApp.Module.Quotes.Authentication.GetToken;

public record GetTokenCommand(User user) : IRequest<ServiceResponse<string>>;

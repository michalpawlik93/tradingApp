using MediatR;
using TradingApp.Application.Models;

namespace TradingApp.Application.Authentication.GetToken;

public record GetTokenCommand(User user) : IRequest<ServiceResponse<string>>;

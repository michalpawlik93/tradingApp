using MediatR;
using TradingApp.Application.Models;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Application.Authorization.AuthorizeProvider;

public record AuthorizeProviderCommand(AuthorizeRequest request)
    : IRequest<ServiceResponse<AuthorizeResponse>>;

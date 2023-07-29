using MediatR;
using TradingApp.Modules.Models;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Modules.Authorization.AuthorizeProvider;

public record AuthorizeProviderCommand(AuthorizeRequest request)
    : IRequest<ServiceResponse<AuthorizeResponse>>;

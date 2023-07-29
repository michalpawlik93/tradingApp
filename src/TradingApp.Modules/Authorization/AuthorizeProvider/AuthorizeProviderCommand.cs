using MediatR;
using TradingApp.Core.Models;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Modules.Authorization.AuthorizeProvider;

public record AuthorizeProviderCommand(AuthorizeRequest request)
    : IRequest<ServiceResponse<AuthorizeResponse>>;

using MediatR;
using Microsoft.Extensions.Logging;
using TradingApp.Modules.Models;
using TradingApp.TradingAdapter.Models;
using TradingApp.TradingViewProvider;

namespace TradingApp.Modules.Authorization.AuthorizeProvider;

public class AuthorizeProviderCommandHandler
    : IRequestHandler<AuthorizeProviderCommand, ServiceResponse<AuthorizeResponse>>
{
    private readonly ITradingViewProvider _tradingViewProvider;
    private readonly ILogger<AuthorizeProviderCommandHandler> _logger;

    public AuthorizeProviderCommandHandler(
        ITradingViewProvider tradingViewProvider,
        ILogger<AuthorizeProviderCommandHandler> logger
    )
    {
        ArgumentNullException.ThrowIfNull(tradingViewProvider);
        ArgumentNullException.ThrowIfNull(logger);
        _tradingViewProvider = tradingViewProvider;
        _logger = logger;
    }

    public async Task<ServiceResponse<AuthorizeResponse>> Handle(
        AuthorizeProviderCommand request,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInformation("{handlerName} started.", nameof(AuthorizeProviderCommandHandler));
        var response = await _tradingViewProvider.Authorize(
            new AuthorizeRequest(request.request.Login, request.request.Password)
        );
        _logger.LogInformation("{handlerName} finished.", nameof(AuthorizeProviderCommandHandler));
        return new ServiceResponse<AuthorizeResponse>(response);
    }
}

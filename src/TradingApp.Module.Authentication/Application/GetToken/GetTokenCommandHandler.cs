using MediatR;
using Microsoft.Extensions.Logging;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Authentication.Abstraction;

namespace TradingApp.Module.Quotes.Authentication.GetToken;

public class GetTokenCommandHandler : IRequestHandler<GetTokenCommand, ServiceResponse<string>>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly ILogger<GetTokenCommandHandler> _logger;

    public GetTokenCommandHandler(IJwtProvider jwtProvider, ILogger<GetTokenCommandHandler> logger)
    {
        ArgumentNullException.ThrowIfNull(jwtProvider);
        ArgumentNullException.ThrowIfNull(logger);
        _jwtProvider = jwtProvider;
        _logger = logger;
    }

    public Task<ServiceResponse<string>> Handle(
        GetTokenCommand request,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInformation("GetTokenCommandHandler started.");
        var getTokenResult = _jwtProvider.Generate(request.user);
        _logger.LogInformation("GetTokenCommandHandler finished.");
        return Task.FromResult(new ServiceResponse<string>(getTokenResult));
    }
}

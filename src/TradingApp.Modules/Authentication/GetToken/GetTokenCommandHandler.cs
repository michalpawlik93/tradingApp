using MediatR;
using Microsoft.Extensions.Logging;
using TradingApp.Modules.Abstraction;
using TradingApp.Modules.Models;

namespace TradingApp.Modules.Authentication.GetToken;

public class GetTokenCommandHandler : IRequestHandler<GetTokenCommand, ServiceResponse<string>>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly ILogger<GetTokenCommandHandler> _logger;

    public GetTokenCommandHandler(IJwtProvider jwtProvider, ILogger<GetTokenCommandHandler> logger)
    {
        _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

using MediatR;
using Microsoft.Extensions.Logging;
using TradingApp.Application.Abstraction;
using TradingApp.Application.Models;

namespace TradingApp.Application.Authentication.GetToken;

public class GetTokenCommandHandler : IRequestHandler<GetTokenCommand, ServiceResponse<string>>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly ILogger<GetTokenCommandHandler> _logger;

    public GetTokenCommandHandler(IJwtProvider jwtProvider, ILogger<GetTokenCommandHandler> logger)
    {
        _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<ServiceResponse<string>> Handle(
        GetTokenCommand request,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInformation("GetTokenCommandHandler started.");
        var getTokenResult = _jwtProvider.Generate(request.user);
        _logger.LogInformation("GetTokenCommandHandler finished.");
        return new ServiceResponse<string>(getTokenResult);
    }
}

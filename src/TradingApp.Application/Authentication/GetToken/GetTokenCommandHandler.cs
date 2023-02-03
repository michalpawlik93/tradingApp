using MediatR;
using TradingApp.Application.Abstraction;
using TradingApp.Application.Models;

namespace TradingApp.Application.Authentication.GetToken;

public class GetTokenCommandHandler : IRequestHandler<GetTokenCommand, ServiceResponse<string>>
{
    private readonly IJwtProvider _jwtProvider;
    public GetTokenCommandHandler(IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
    }
    public async Task<ServiceResponse<string>> Handle(GetTokenCommand request, CancellationToken cancellationToken)
    {
        var getTokenResult = _jwtProvider.Generate(request.user);
        return new ServiceResponse<string>(getTokenResult);
    }
}

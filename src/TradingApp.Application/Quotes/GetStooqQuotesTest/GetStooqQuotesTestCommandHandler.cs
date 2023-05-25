using MediatR;
using Microsoft.Extensions.Logging;
using TradingApp.Application.Models;
using TradingApp.StooqProvider;
using TradingApp.TradingAdapter.Enums;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Application.Quotes.GetStooqQuotesTest;

public class GetStooqQuotesTestCommandHandler : IRequestHandler<GetStooqQuotesTestCommand, ServiceResponse<IEnumerable<Quote>>>
{
    private readonly IStooqProvider _provider;
    private readonly ILogger<GetStooqQuotesTestCommandHandler> _logger;
    public GetStooqQuotesTestCommandHandler(IStooqProvider provider, ILogger<GetStooqQuotesTestCommandHandler> logger)
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(logger);
        _provider = provider;
        _logger = logger;
    }

    public async Task<ServiceResponse<IEnumerable<Quote>>> Handle(GetStooqQuotesTestCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{handlerName} started.", nameof(GetStooqQuotesTestCommandHandler));
        var saveResponse = await _provider.SaveQuotes(HistoryType.Daily);
        _logger.LogInformation("{handlerName} finished.", nameof(GetStooqQuotesTestCommandHandler));
        var getQuotesResponse = await _provider.GetQuotes(HistoryType.Daily);
        return new ServiceResponse<IEnumerable<Quote>>(getQuotesResponse);
    }
}
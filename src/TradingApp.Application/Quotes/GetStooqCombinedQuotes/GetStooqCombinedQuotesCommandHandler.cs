using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using TradingApp.Application.Models;
using TradingApp.StooqProvider;
using TradingApp.TradingAdapter.Constants;
using TradingApp.TradingAdapter.Enums;
using TradingApp.TradingAdapter.Evaluator;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Application.Quotes.GetStooqQuotes;

public class GetStooqCombinedQuotesCommandHandler : IRequestHandler<GetStooqCombinedQuotesCommand, ServiceResponse<GetStooqCombinedQuotesResponse>>
{
    private readonly IStooqProvider _provider;
    private readonly ILogger<GetStooqCombinedQuotesCommandHandler> _logger;
    private readonly ISkenderEvaluator _skenderEvaluator;
    private const int RSIPeriod = 14;
    public GetStooqCombinedQuotesCommandHandler(IStooqProvider provider, ILogger<GetStooqCombinedQuotesCommandHandler> logger,
        ISkenderEvaluator skenderEvaluator)
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(skenderEvaluator);
        _provider = provider;
        _logger = logger;
        _skenderEvaluator = skenderEvaluator;
    }

    public async Task<ServiceResponse<GetStooqCombinedQuotesResponse>> Handle(GetStooqCombinedQuotesCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{handlerName} started.", nameof(GetStooqCombinedQuotesCommandHandler));
        var getQuotesResponse = await _provider.GetQuotes(HistoryType.Daily);
        if (getQuotesResponse.IsFailed)
        {
            return new ServiceResponse<GetStooqCombinedQuotesResponse>(getQuotesResponse.ToResult());
        }
        var rsiResults = _skenderEvaluator.GetRSI(getQuotesResponse.Value, RSIPeriod);
        var smaResults = _skenderEvaluator.GetSMA(getQuotesResponse.Value);
        var combinedResults = getQuotesResponse.Value
            .Select((q, i) => new CombinedQuote(q, rsiResults.ElementAt(i), smaResults.ElementAt(i)))
            .ToList();
        return new ServiceResponse<GetStooqCombinedQuotesResponse>(Result.Ok(
            new GetStooqCombinedQuotesResponse(combinedResults, new RsiSettings(RsiSettingsConst.Oversold, RsiSettingsConst.Overbought))));
    }
}
using FluentResults;
using MediatR;
using Quartz;
using TradingApp.EvaluationScheduler.Utils;
using TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Application.Services;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.EvaluationScheduler.Jobs;

public class Evaluate5MinJob : IJob
{
    private readonly ITradingAdapter _provider;
    private readonly IMediator _mediator;

    public Evaluate5MinJob(ITradingAdapter provider, IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(provider);
        ArgumentNullException.ThrowIfNull(mediator);
        _provider = provider;
        _mediator = mediator;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync($"{nameof(Evaluate5MinJob)} started...");
        var getQuotesResponse = await GetQuotes();
        if (getQuotesResponse.IsFailed)
        {
            await ConsoleUtils.WriteResultMessages(getQuotesResponse);
            return;
        }
        var evaluateSRsiResponse = await _mediator.Send(new EvaluateSRsiCommand(getQuotesResponse.Value.ToList()));
        await ConsoleUtils.WriteMessages(evaluateSRsiResponse);
        await Console.Out.WriteLineAsync($"{nameof(Evaluate5MinJob)} finished.");
    }

    private Task<Result<IEnumerable<Quote>>> GetQuotes()
    {
        var startDate = new DateTime(2023, 5, 20, 0, 0, 0, DateTimeKind.Utc);
        var timeFrame = new TimeFrame(Granularity.FiveMins, startDate, startDate.AddHours(5));
        var asset = new Asset(AssetName.BTC, AssetType.Cryptocurrency);
        return _provider.GetQuotes(
            new GetQuotesRequest(timeFrame, asset, new PostProcessing(true))
        );
    }

}

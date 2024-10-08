﻿using FluentResults;
using MediatR;
using Quartz;
using TradingApp.Domain.Modules.Constants;
using TradingApp.EvaluationScheduler.Utils;
using TradingApp.Module.Quotes.Application.Features.EvaluateCipherB;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.Module.Quotes.Domain.Constants;

namespace TradingApp.EvaluationScheduler.Jobs;

public class Evaluate5MinJCipherBJob : IJob
{
    private readonly IMediator _mediator;
    private readonly ITradingAdapter _adapter;

    public Evaluate5MinJCipherBJob(IMediator mediator, ITradingAdapter adapter)
    {
        ArgumentNullException.ThrowIfNull(mediator);
        ArgumentNullException.ThrowIfNull(adapter);
        _mediator = mediator;
        _adapter = adapter;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync($"{nameof(Evaluate5MinJCipherBJob)} started...");
        var getQuotesResponse = await GetQuotes();
        if (getQuotesResponse.IsFailed)
        {
            await ConsoleUtils.WriteResultMessages(getQuotesResponse);
            return;
        }
        var evaluateResponse = await _mediator.Send(
            new EvaluateCipherBCommand(
                getQuotesResponse.Value.ToList(),
                Granularity.FiveMins,
                AssetName.EURPLN,
                new SettingsRequest(
                    SRsiSettingsConst.SRsiSettingsDefault,
                    null,
                    MfiSettingsConst.MfiSettingsDefault,
                    WaveTrendSettingsConst.WaveTrendSettingsDefault
                )
            )
        );
        await ConsoleUtils.WriteResultMessages(evaluateResponse);
        await Console.Out.WriteLineAsync($"{nameof(Evaluate5MinJCipherBJob)} finished.");
    }

    private async Task<IResult<IEnumerable<Quote>>> GetQuotes()
    {
        var startDate = new DateTime(2023, 5, 20, 0, 0, 0, DateTimeKind.Utc);
        var timeFrame = new TimeFrame(Granularity.FiveMins, startDate, startDate.AddHours(5));
        var asset = new Asset(AssetName.BTC, AssetType.Cryptocurrency);

        return await _adapter.GetQuotes(
            timeFrame,
            asset,
            new PostProcessing(true),
            CancellationToken.None
        );
    }
}

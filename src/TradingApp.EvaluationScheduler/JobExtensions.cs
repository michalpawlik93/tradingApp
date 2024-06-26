﻿using Quartz;
using System.Diagnostics.CodeAnalysis;
using TradingApp.EvaluationScheduler.Jobs;

namespace TradingApp.EvaluationScheduler;

[ExcludeFromCodeCoverage]
public static class JobExtensions
{
    public static IJobDetail CreateJob() =>
         JobBuilder.Create<Evaluate5MinJCipherBJob>()
            .WithIdentity(name: "Evaluate5MinJCipherBJob", group: "JobGroup")
            .Build();

    public static ITrigger CreateTrigger() =>
        TriggerBuilder.Create()
            .WithIdentity(name: "RepeatingTrigger", group: "TriggerGroup")
            .WithSimpleSchedule(o => o
                .RepeatForever()
                .WithIntervalInSeconds(60))
            .Build();
}

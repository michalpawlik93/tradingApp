using Quartz;
using TradingApp.EvaluationScheduler.Jobs;

namespace TradingApp.EvaluationScheduler;

public static class JobExtensions
{
    public static IJobDetail CreateJob() =>
         JobBuilder.Create<EvaluateJob>()
            .WithIdentity(name: "EvaluateJob", group: "JobGroup")
            .Build();

    public static ITrigger CreateTrigger() =>
        TriggerBuilder.Create()
            .WithIdentity(name: "RepeatingTrigger", group: "TriggerGroup")
            .WithSimpleSchedule(o => o
                .RepeatForever()
                .WithIntervalInSeconds(5))
            .Build();
}

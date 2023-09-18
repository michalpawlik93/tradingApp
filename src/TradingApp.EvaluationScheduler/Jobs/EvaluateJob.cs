using Quartz;

namespace TradingApp.EvaluationScheduler.Jobs;

public class EvaluateJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await Console.Out.WriteLineAsync("Executing background job");
    }
}
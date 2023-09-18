using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using TradingApp.Core.Models;
using TradingApp.EvaluationScheduler;
using TradingApp.Evaluator;
using TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;
using TradingApp.Module.Quotes.Application.Services;
using TradingApp.Module.Quotes.Ports;

var builder = Host.CreateDefaultBuilder()
    .ConfigureServices(
        (cxt, services) =>
        {
            services.AddQuartz();
            services.AddQuartzHostedService(opt =>
            {
                opt.WaitForJobsToComplete = true;
            });
            services.AddSingleton<IEvaluator, CustomEvaluator>();
            services.AddTransient<IDecisionService, DecisionService>();
            services.AddScoped<
                IRequestHandler<EvaluateSRsiCommand, ServiceResponse>,
                EvaluateSRsiCommandHandler
            >();
        }
    )
    .Build();

var schedulerFactory = builder.Services.GetRequiredService<ISchedulerFactory>();
var scheduler = await schedulerFactory.GetScheduler();

await scheduler.ScheduleJob(JobExtensions.CreateJob(), JobExtensions.CreateTrigger());
await builder.RunAsync();

//https://code-maze.com/schedule-jobs-with-quartz-net/
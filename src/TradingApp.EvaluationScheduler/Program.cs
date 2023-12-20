using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using TradingApp.Core.Models;
using TradingApp.EvaluationScheduler;
using TradingApp.Evaluator;
using TradingApp.Module.Quotes.Application.Features.EvaluateSrsi;
using TradingApp.Module.Quotes.Application.Services;
using TradingApp.Module.Quotes.Ports;
using TradingApp.MongoDb.Extensions;
using TradingApp.StooqProvider.Setup;

var builder = Host.CreateDefaultBuilder()
     .ConfigureAppConfiguration((hostingContext, config) =>
     {
         config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
         config.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
         config.AddEnvironmentVariables();
     })
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
            services.AddStooqProvider(cxt.Configuration);
            services.AddMongoDbService(cxt.Configuration);
        }
    )
    .Build();

var schedulerFactory = builder.Services.GetRequiredService<ISchedulerFactory>();
var scheduler = await schedulerFactory.GetScheduler();

await scheduler.ScheduleJob(JobExtensions.CreateJob(), JobExtensions.CreateTrigger());
await builder.RunAsync();
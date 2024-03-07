using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using TradingApp.EvaluationScheduler;
using TradingApp.Module.Quotes.Configuration;

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
            services.AddQuotoesServices(cxt.Configuration);
        }
    )
    .Build();

var schedulerFactory = builder.Services.GetRequiredService<ISchedulerFactory>();
var scheduler = await schedulerFactory.GetScheduler();

await scheduler.ScheduleJob(JobExtensions.CreateJob(), JobExtensions.CreateTrigger());
await builder.RunAsync();
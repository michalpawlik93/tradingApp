using MassTransit;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
using TradingApp.Core.Configuration;

namespace TradingApp.Core.Extensions;

[ExcludeFromCodeCoverage]
public static class EventBusExtensions
{
    public static IServiceCollection AddDomainEventBus(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Ping).Assembly);
            config.NotificationPublisher = new TaskWhenAllPublisher();
        });
        return services;
    }

    public static IServiceCollection AddIntegrationEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqSettings = configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
        ArgumentNullException.ThrowIfNull(rabbitMqSettings);
        return services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", c =>
                {
                    c.Username(rabbitMqSettings.Username);
                    c.Password(rabbitMqSettings.Password);
                });
                //cfg.Publish<IIntegrationEvent>(x => { });
            });
        });
    }
}
//https://masstransit.io/documentation/configuration
//https://masstransit.io/documentation/configuration/multibus
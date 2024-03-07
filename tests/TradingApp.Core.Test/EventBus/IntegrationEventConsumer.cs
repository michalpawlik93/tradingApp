using MassTransit;
using TradingApp.Core.EventBus.Events;

namespace TradingApp.Core.Test.EventBus;

public class IntegrationEventConsumer : IConsumer<IIntegrationEvent>
{
    public async Task Consume(ConsumeContext<IIntegrationEvent> context)
    {
        var integrationEvent = context.Message;

        Console.WriteLine($"Received integration event: {integrationEvent.GetType().Name}");

    }
}

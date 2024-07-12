using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using TestUtils.Fixtures;
using Xunit;

namespace TradingApp.TestUtils.Fixtures;

public class EventBusFixture : IAsyncLifetime
{
    public IBus BusControl { get; private set; }
    public ITestHarness TestHarness { get; private set; }
    public IConsumerTestHarness<TestIntegrationEventConsumer> ConsumerHarness { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public EventBusFixture()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public async Task InitializeAsync()
    {
        await using var provider = new ServiceCollection()
       .AddMassTransitTestHarness(x =>
       {
           x.AddConsumer<TestIntegrationEventConsumer>();
       })
       .BuildServiceProvider(true);

        var harness = provider.GetRequiredService<ITestHarness>();
        BusControl = harness.Bus;
        await harness.Start();
        TestHarness = harness;
        ConsumerHarness = harness.GetConsumerHarness<TestIntegrationEventConsumer>();
    }

    public async Task DisposeAsync()
    {
        await TestHarness.Stop();
    }

    public class TestIntegrationEventConsumer : IConsumer<TestIntegrationEvent>
    {
        public bool ConsumerReceivedMessage { get; private set; }

        public Task Consume(ConsumeContext<TestIntegrationEvent> context)
        {
            ConsumerReceivedMessage = true;
            return Task.CompletedTask;
        }
    }
}


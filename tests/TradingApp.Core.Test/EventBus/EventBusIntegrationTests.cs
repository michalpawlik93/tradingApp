using MediatR;
using NSubstitute;
using TestUtils.Collections;
using TradingApp.TestUtils.Fixtures;
using EventBusTest = TradingApp.Core.EventBus.EventBus;

namespace TradingApp.Core.Test.EventBus;

[Collection(nameof(RabbitMqFixtureCollection))]
public class EventBusIntegrationTests : IClassFixture<EventBusFixture>
{
    private readonly EventBusFixture _fixture;
    private readonly EventBusTest _sut;
    private readonly IMediator _mediator = Substitute.For<IMediator>();

    public EventBusIntegrationTests(EventBusFixture fixture)
    {
        _fixture = fixture;
        _sut = new EventBusTest(_mediator, fixture.BusControl);
    }

    //[Fact]
    //public async Task Publish_MessagePublishedToIntegrationBus_PublishedToDomainBus()
    //{
    //    //Arrange
    //    var aggregate = new TestAggregate();
    //    //Act
    //    await _sut.Publish(aggregate, CancellationToken.None);
    //    //Assert
    //    await _mediator
    //        .Received(1)
    //        .Publish(Arg.Any<TestDomainEvent>(), Arg.Any<CancellationToken>());
    //    var consumed = await _fixture.ConsumerHarness.Consumed.Any<TestIntegrationEvent>();
    //    consumed.Should().BeTrue();
    //}

    //[Fact]
    //public async Task Publish_ShouldPublishIntegrationEvent()
    //{
    //    // Arrange
    //    var harness = new InMemoryTestHarness();

    //    await harness.Start();

    //    var publishEndPoint = harness.Bus;

    //    var eventBus = new EventBusTest(_mediator, publishEndPoint);

    //    var aggregate = new TestAggregate();

    //    // Act
    //    await eventBus.Publish(aggregate, CancellationToken.None);

    //    // Assert
    //    var received = await harness.Consumed.Any<TestIntegrationEvent>();
    //    received.Should().BeTrue();

    //    await harness.Stop();
    //}
}

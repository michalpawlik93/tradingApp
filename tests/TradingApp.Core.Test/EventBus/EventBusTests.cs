using MassTransit;
using MediatR;
using NSubstitute;
using TestUtils.Fixtures;
using TradingApp.Core.EventBus.Events;
using EventBusTest = TradingApp.Core.EventBus.EventBus;

namespace TradingApp.Core.Test.EventBus;

public class EventBusTests
{
    private readonly EventBusTest _sut;
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly IPublishEndpoint _publishEndpoint = Substitute.For<IPublishEndpoint>();

    public EventBusTests()
    {
        _sut = new EventBusTest(_mediator, _publishEndpoint);
    }

    [Fact]
    public async Task Publish_MessagePublishedToIntegrationBus_PublishedToDomainBus()
    {
        //Arrange
        var aggregate = new TestAggregate();
        //Act
        await _sut.Publish(aggregate, CancellationToken.None);
        //Assert
        await _mediator
            .Received(1)
            .Publish(Arg.Any<TestDomainEvent>(), Arg.Any<CancellationToken>());
        await _publishEndpoint
            .Received(1)
            .Publish(Arg.Any<IIntegrationEvent>(), Arg.Any<CancellationToken>());
    }
}

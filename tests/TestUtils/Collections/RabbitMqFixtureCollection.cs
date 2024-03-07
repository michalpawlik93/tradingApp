using TradingApp.TestUtils.Fixtures;
using Xunit;

namespace TestUtils.Collections;

[CollectionDefinition(nameof(RabbitMqFixtureCollection))]
public class RabbitMqFixtureCollection : ICollectionFixture<EventBusFixture> { }

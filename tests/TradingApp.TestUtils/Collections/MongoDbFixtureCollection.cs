using TradingApp.TestUtils.Fixtures;
using Xunit;

namespace TradingApp.TestUtils.Collections;

[CollectionDefinition(nameof(MongoDbFixtureCollection))]
public class MongoDbFixtureCollection : ICollectionFixture<MongoDbFixture> { }

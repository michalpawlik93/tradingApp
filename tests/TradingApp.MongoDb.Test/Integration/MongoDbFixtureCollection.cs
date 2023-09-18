using TradingApp.MongoDb.Test.Fixtures;

namespace TradingApp.MongoDb.Test.Integration;

[CollectionDefinition(nameof(MongoDbFixtureCollection))]
public class MongoDbFixtureCollection : ICollectionFixture<MongoDbFixture> { }

using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using MongoDB.Driver;
using Xunit;

namespace TradingApp.TestUtils.Fixtures;

public class MongoDbFixture : IAsyncLifetime
{
    private readonly IContainer _dockerContainer;
    private readonly int _randomPort = Random.Shared.Next(10_000, 60_000);

    public MongoDbFixture()
    {
        _dockerContainer = new ContainerBuilder()
            .WithName($"decisions-{Guid.NewGuid():D}")
            .WithImage("mongo:latest")
            .WithPortBinding(_randomPort, 27017)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(27017))
            .Build();

        var mongoClientSettings = new MongoClientSettings
        {
            Server = new MongoServerAddress(
                "localhost",
                _randomPort
            ),
        };

        var mongoClient = new MongoClient(mongoClientSettings);
        MongoDatabase = mongoClient.GetDatabase("tradingapp");
    }

    public IMongoDatabase MongoDatabase { get; }

    public async Task DisposeAsync()
    {
        await _dockerContainer.StopAsync();
    }

    public async Task InitializeAsync()
    {
        await _dockerContainer.StartAsync();
    }
}

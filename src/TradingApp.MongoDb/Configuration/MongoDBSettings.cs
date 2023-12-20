namespace TradingApp.MongoDb.Configuration;

public class MongoDBSettings
{
    public const string ConfigSectionName = "MongoDB";
    public string ConnectionURI { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;

}

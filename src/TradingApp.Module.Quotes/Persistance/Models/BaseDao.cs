using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TradingApp.Module.Quotes.Persistance.Models;
public class BaseDao
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
}

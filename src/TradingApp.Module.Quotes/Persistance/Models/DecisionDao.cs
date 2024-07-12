using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TradingApp.Core.Attributes;

namespace TradingApp.Module.Quotes.Persistance.Models;

[DataCollection("decisions")]
public class DecisionDao : BaseDao
{
    [BsonRepresentation(BsonType.DateTime)]
    public DateTimeOffset TimeStamp { get; set; }

    [BsonRepresentation(BsonType.String)]
    public string Action { get; set; }

    [BsonRepresentation(BsonType.String)]
    public string MarketDirection { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal IndexOutcomeValue { get; set; }

    [BsonRepresentation(BsonType.String)]
    public string IndexOutcomeName { get; set; }
}
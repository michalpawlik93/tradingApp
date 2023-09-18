namespace TradingApp.Core.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class DataCollectionAttribute : Attribute
{
    public string CollectionName { get; }

    public DataCollectionAttribute(string collectionName)
    {
        CollectionName = collectionName;
    }
}

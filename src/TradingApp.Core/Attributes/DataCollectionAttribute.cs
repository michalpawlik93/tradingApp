using System.Diagnostics.CodeAnalysis;

namespace TradingApp.Core.Attributes;

[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class DataCollectionAttribute : Attribute
{
    public string CollectionName { get; }

    public DataCollectionAttribute(string collectionName)
    {
        CollectionName = collectionName;
    }
}

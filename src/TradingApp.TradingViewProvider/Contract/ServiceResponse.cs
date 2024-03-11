using System.Diagnostics.CodeAnalysis;

namespace TradingApp.TradingViewProvider.Contract;

[ExcludeFromCodeCoverage]
public class ServiceResponse<T> : ServiceResponseBase
{
    /// <summary>
    /// body
    /// </summary>
    public T d { get; set; }
}

[ExcludeFromCodeCoverage]
public class ServiceResponseBase
{
    /// <summary>
    /// status
    /// </summary>
    public string s { get; set; }
    public string errmsg { get; set; }
}
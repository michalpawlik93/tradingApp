namespace TradingApp.TradingViewProvider.Contract;

public class ServiceResponse<T> : ServiceResponseBase
{
    /// <summary>
    /// body
    /// </summary>
    public T d { get; set; }
}

public class ServiceResponseBase
{
    /// <summary>
    /// status
    /// </summary>
    public string s { get; set; }
    public string errmsg { get; set; }
}
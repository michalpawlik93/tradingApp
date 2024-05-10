using FluentResults;

namespace TradingApp.Core.Models;

public class BadRequestError : Error
{
    public BadRequestError(string message)
        : base(message)
    {
        Metadata.Add("ErrorCode", 400);
    }
}

public class SystemError : Error
{
    public SystemError(string message)
        : base(message)
    {
        Metadata.Add("ErrorCode", 500);
    }
}

public class NotFoundError : Error
{
    public NotFoundError(string message)
        : base(message)
    {
        Metadata.Add("ErrorCode", 404);
    }
}
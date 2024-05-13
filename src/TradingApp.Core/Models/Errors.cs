using FluentResults;
using Microsoft.AspNetCore.Http;

namespace TradingApp.Core.Models;

public class ValidationError : Error
{
    public ValidationError(string message)
        : base(message)
    {
        Metadata.Add("ErrorCode", StatusCodes.Status400BadRequest);
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
        Metadata.Add("ErrorCode", StatusCodes.Status404NotFound);
    }
}
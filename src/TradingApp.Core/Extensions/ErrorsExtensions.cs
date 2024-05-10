using FluentResults;
using TradingApp.Core.Enums;
using TradingApp.Core.Models;

namespace TradingApp.Core.Extensions;

public static class ErrorsExtensions
{
    public static string GetErrorServiceResponseMessage(this IError error)
    {
        return error switch
        {
            BadRequestError _ => MessageType.BadRequest,
            SystemError _ => MessageType.Error,
            NotFoundError _ => MessageType.NotFound,
            _ => MessageType.Error
        };
    }
}

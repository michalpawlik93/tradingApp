using FluentResults;
using TradingApp.Application.Models;

namespace TradingApp.Application.Errors;

public class UserError : Error
{
    public UserError(User user)
        : base($"{nameof(user.ApiKey)} and {nameof(user.ApiSecret)} can not be null.")
    {
        Metadata.Add("ErrorCode", "1");
    }
}
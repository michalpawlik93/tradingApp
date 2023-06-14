using FluentResults;
using System.Diagnostics.CodeAnalysis;
using TradingApp.Application.Models;

namespace TradingApp.Application.Errors;

[ExcludeFromCodeCoverage]
public class UserError : Error
{
    public UserError(User user)
        : base($"{nameof(user.Name)} and {nameof(user.ApiSecret)} can not be null.")
    {
        Metadata.Add("ErrorCode", "1");
    }
}

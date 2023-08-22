using FluentResults;
using System.Diagnostics.CodeAnalysis;
using TradingApp.Modules.Authentication.Models;

namespace TradingApp.Modules.Authentication.Errors;

[ExcludeFromCodeCoverage]
public class UserError : Error
{
    public UserError()
        : base($"{nameof(User.Name)} and {nameof(User.ApiSecret)} can not be null.")
    {
        Metadata.Add("ErrorCode", "1");
    }
}

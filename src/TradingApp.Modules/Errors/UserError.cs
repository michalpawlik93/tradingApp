using FluentResults;
using System.Diagnostics.CodeAnalysis;
using TradingApp.Modules.Models;

namespace TradingApp.Modules.Errors;

[ExcludeFromCodeCoverage]
public class UserError : Error
{
    public UserError()
        : base($"{nameof(User.Name)} and {nameof(User.ApiSecret)} can not be null.")
    {
        Metadata.Add("ErrorCode", "1");
    }
}

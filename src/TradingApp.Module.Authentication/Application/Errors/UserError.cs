using FluentResults;
using System.Diagnostics.CodeAnalysis;
using TradingApp.Module.Quotes.Authentication.Models;

namespace TradingApp.Module.Quotes.Authentication.Errors;

[ExcludeFromCodeCoverage]
public class UserError : Error
{
    public UserError()
        : base($"{nameof(User.Name)} and {nameof(User.ApiSecret)} can not be null.")
    {
        Metadata.Add("ErrorCode", "1");
    }
}

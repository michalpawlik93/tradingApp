using FluentResults;
using TradingApp.Module.Quotes.Authentication.Errors;

namespace TradingApp.Module.Quotes.Authentication.Models;

public class User
{
    private static string ValidatioErrorMessage = "Error occured when validating user model.";

    public string Name { get; set; }
    public string ApiSecret { get; set; }

    public Result<string> Validate()
    {
        if (Name is null || ApiSecret is null)
        {
            return Result.Fail<string>(ValidatioErrorMessage).WithError(new UserError());
        }
        return Result.Ok();
    }
}

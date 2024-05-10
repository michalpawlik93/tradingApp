using System.Diagnostics.CodeAnalysis;

namespace TradingApp.Core.Enums;

[ExcludeFromCodeCoverage]
public static class MessageType
{
    public const string Error = "Error";
    public const string BadRequest = "BadRequest";
    public const string Success = "Success";
    public const string NotFound = "NotFound";
}

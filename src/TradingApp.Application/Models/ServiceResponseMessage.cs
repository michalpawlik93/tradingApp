using System.Diagnostics.CodeAnalysis;

namespace TradingApp.Application.Models;

/// <summary>
/// The message associated to the service response
/// </summary>
/// 
[ExcludeFromCodeCoverage]
public class ServiceResponseMessage
{
    /// <summary>
    /// The message
    /// </summary>
    /// <example>Success</example>
    public string Message { get; set; }

    /// <summary>
    /// Type of the message
    /// One of Info, Warning, Error, Forbidden
    /// </summary>
    /// <example>Info</example>
    public string Type { get; set; }

    public ServiceResponseMessage() { }
    public ServiceResponseMessage(string message, string type)
    {
        Message = message;
        Type = type;
    }
}


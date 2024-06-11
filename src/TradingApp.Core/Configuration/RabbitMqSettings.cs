using System.Diagnostics.CodeAnalysis;

namespace TradingApp.Core.Configuration;

[ExcludeFromCodeCoverage]
public class RabbitMqSettings
{
    public string Host { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

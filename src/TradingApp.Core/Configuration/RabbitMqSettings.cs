using System.Diagnostics.CodeAnalysis;

namespace TradingApp.Core.Configuration;

[ExcludeFromCodeCoverage]
public class RabbitMqSettings
{
    public string Uri { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
}

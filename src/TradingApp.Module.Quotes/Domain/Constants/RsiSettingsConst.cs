using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Domain.Modules.Constants;

public static class RsiSettingsConst
{
    public const decimal Overbought = 60;
    public const decimal Oversold = -60;
    public const int DefaultPeriod = 14;

    public static readonly RsiSettings RsiSettingsDefault =
        new(Oversold, Overbought, true, DefaultPeriod);
}

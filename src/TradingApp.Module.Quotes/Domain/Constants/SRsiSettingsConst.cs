using TradingApp.Module.Quotes.Application.Models;

namespace TradingApp.Module.Quotes.Domain.Constants;

public static class SRsiSettingsConst
{
    public const decimal Overbought = 60;
    public const decimal Oversold = -60;
    public const int ChannelLength = 14;
    public const int StochKSmooth = 3;
    public const int StochDSmooth = 3;

    public static readonly SRsiSettings SRsiSettingsDefault = new(true, ChannelLength, StochKSmooth, StochDSmooth, Oversold, Overbought);
}

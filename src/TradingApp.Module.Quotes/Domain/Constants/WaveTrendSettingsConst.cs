using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Domain.Modules.Constants;

public static class WaveTrendSettingsConst
{
    public const int ChannelLength = 10;
    public const int AverageLength = 21;
    public const int MovingAverageLength = 3;

    public const int Oversold = -60;
    public const int OversoldLevel2 = -53;
    public const int Overbought = 60;
    public const int OverboughtLevel2 = 53;

    public static WaveTrendSettings WaveTrendSettingsDefault =
        new(
            Oversold,
            Overbought,
            OversoldLevel2,
            OverboughtLevel2,
            ChannelLength,
            AverageLength,
            MovingAverageLength
        );
}

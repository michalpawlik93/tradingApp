using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Domain.Constants;

public static class MfiSettingsConst
{
    public const int ChannelLength = 60;
    public const int ScaleFactor = 150;

    public static MfiSettings MfiSettingsDefault = new(ChannelLength, ScaleFactor);
}

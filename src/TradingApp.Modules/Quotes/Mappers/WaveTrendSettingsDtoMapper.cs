using TradingApp.Modules.Quotes.Models;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Modules.Quotes.Mappers;

public static class WaveTrendSettingsDtoMapper
{
    public static WaveTrendSettings ToDomainModel(WaveTrendSettingsDto dto)
    {
        return new WaveTrendSettings(dto.Oversold, dto.Overbought, dto.ChannelLength, dto.AverageLength, dto.MovingAverageLength);
    }
}

using TradingApp.Modules.Quotes.Models;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Modules.Quotes.Mappers;

public static class SRsiSettingsDtoMapper
{
    public static SRsiSettings ToDomainModel(SRsiSettingsDto dto)
    {
        return new SRsiSettings(dto.Enable, dto.Length, dto.StochKSmooth, dto.StochDSmooth, dto.Oversold, dto.Overbought);
    }
}

using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Models;

namespace TradingApp.Module.Quotes.Application.Mappers;

public static class SRsiSettingsDtoMapper
{
    public static SRsiSettings ToDomainModel(SRsiSettingsDto dto)
    {
        return new SRsiSettings(dto.Enable, dto.Length, dto.StochKSmooth, dto.StochDSmooth, dto.Oversold, dto.Overbought);
    }
}

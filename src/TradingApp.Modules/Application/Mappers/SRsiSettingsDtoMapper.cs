using TradingApp.Modules.Application.Dtos;
using TradingApp.Modules.Application.Models;

namespace TradingApp.Modules.Application.Mappers;

public static class SRsiSettingsDtoMapper
{
    public static SRsiSettings ToDomainModel(SRsiSettingsDto dto)
    {
        return new SRsiSettings(dto.Enable, dto.Length, dto.StochKSmooth, dto.StochDSmooth, dto.Oversold, dto.Overbought);
    }
}
